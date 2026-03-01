using LogiTrack.Api.Contracts.Orders;
using LogiTrack.Api.Contracts.Mapping;
using LogiTrack.Api.Data;
using LogiTrack.Api.Models;
using LogiTrack.Api.Services.Caching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace LogiTrack.Api.Services.Orders;

public class OrderService : IOrderService
{
    private readonly LogiTrackDbContext _context;
    private readonly IMemoryCache _cache;

    public OrderService(LogiTrackDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<List<OrderResponseDto>> GetAllAsync()
    {
        if (!_cache.TryGetValue(CacheKeys.OrdersAll, out List<OrderResponseDto>? orders))
        {
            var entities = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.InventoryItem)
                .AsNoTracking()
                .ToListAsync();

            orders = entities.Select(o => o.ToResponseDto()).ToList();

            _cache.Set(
                CacheKeys.OrdersAll,
                orders,
                new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5)));
        }

        return orders ?? new List<OrderResponseDto>();
    }

    public async Task<OrderResponseDto?> GetByIdAsync(int orderId)
    {
        var cacheKey = CacheKeys.OrderById(orderId);

        if (!_cache.TryGetValue(cacheKey, out OrderResponseDto? orderDto))
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.InventoryItem)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order is null)
                return null;

            orderDto = order.ToResponseDto();

            _cache.Set(
                cacheKey,
                orderDto,
                new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5)));
        }

        return orderDto;
    }

    public async Task<OrderServiceResult> CreateAsync(CreateOrderDto dto)
    {
        try
        {
            var inventoryIds = dto.OrderItems
                .Select(i => i.InventoryItemId)
                .Distinct()
                .ToList();

            var inventoryMap = await _context.InventoryItems
                .Where(i => inventoryIds.Contains(i.ItemId))
                .ToDictionaryAsync(i => i.ItemId);

            var order = new Order();
            order.UpdateCustomerName(dto.CustomerName);

            foreach (var itemDto in dto.OrderItems)
            {
                if (!inventoryMap.TryGetValue(itemDto.InventoryItemId, out var inventory))
                {
                    return OrderServiceResult.Failure(
                        OrderErrorCode.InventoryItemNotFound,
                        $"Inventory item {itemDto.InventoryItemId} does not exist.");
                }

                if (!inventory.DecreaseStock(itemDto.QuantityOrdered))
                {
                    return OrderServiceResult.Failure(
                        OrderErrorCode.InsufficientStock,
                        $"Not enough stock for item {inventory.ItemId}.");
                }

                var orderItem = new OrderItem
                {
                    InventoryItemId = itemDto.InventoryItemId,
                    InventoryItem = inventory
                };

                orderItem.SetValues(itemDto.QuantityOrdered, inventory.Price);
                order.AddItem(orderItem);
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            InvalidateCache(order.OrderId);

            var savedOrder = await LoadOrderAsync(order.OrderId);
            if (savedOrder is null)
            {
                return OrderServiceResult.Failure(
                    OrderErrorCode.OrderNotFound,
                    $"Order {order.OrderId} could not be loaded after creation.");
            }

            return OrderServiceResult.Successful(savedOrder.ToResponseDto());
        }
        catch (ArgumentException ex)
        {
            return OrderServiceResult.Failure(OrderErrorCode.ValidationError, ex.Message);
        }
    }

    public async Task<OrderServiceResult> AddItemAsync(int orderId, CreateOrderItemDto dto)
    {
        try
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.InventoryItem)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order is null)
            {
                return OrderServiceResult.Failure(
                    OrderErrorCode.OrderNotFound,
                    $"Order {orderId} not found.");
            }

            var inventory = await _context.InventoryItems.FindAsync(dto.InventoryItemId);
            if (inventory is null)
            {
                return OrderServiceResult.Failure(
                    OrderErrorCode.InventoryItemNotFound,
                    $"Inventory item {dto.InventoryItemId} does not exist.");
            }

            if (!inventory.DecreaseStock(dto.QuantityOrdered))
            {
                return OrderServiceResult.Failure(
                    OrderErrorCode.InsufficientStock,
                    $"Not enough stock for item {inventory.ItemId}.");
            }

            var existing = order.OrderItems.FirstOrDefault(i => i.InventoryItemId == dto.InventoryItemId);

            if (existing is not null)
            {
                existing.IncreaseQuantity(dto.QuantityOrdered);
            }
            else
            {
                var newItem = new OrderItem
                {
                    InventoryItemId = dto.InventoryItemId,
                    InventoryItem = inventory
                };

                newItem.SetValues(dto.QuantityOrdered, inventory.Price);
                order.AddItem(newItem);
            }

            await _context.SaveChangesAsync();
            InvalidateCache(orderId);

            var savedOrder = await LoadOrderAsync(orderId);
            if (savedOrder is null)
            {
                return OrderServiceResult.Failure(
                    OrderErrorCode.OrderNotFound,
                    $"Order {orderId} could not be loaded after update.");
            }

            return OrderServiceResult.Successful(savedOrder.ToResponseDto());
        }
        catch (ArgumentException ex)
        {
            return OrderServiceResult.Failure(OrderErrorCode.ValidationError, ex.Message);
        }
    }

    public async Task<OrderServiceResult> RemoveItemAsync(int orderId, int inventoryItemId)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.InventoryItem)
            .FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (order is null)
        {
            return OrderServiceResult.Failure(
                OrderErrorCode.OrderNotFound,
                $"Order {orderId} not found.");
        }

        var orderItem = order.OrderItems.FirstOrDefault(i => i.InventoryItemId == inventoryItemId);
        if (orderItem is null)
        {
            return OrderServiceResult.Failure(
                OrderErrorCode.OrderItemNotFound,
                $"Order item with inventory item {inventoryItemId} not found.");
        }

        orderItem.InventoryItem?.IncreaseStock(orderItem.QuantityOrdered);
        order.RemoveItem(orderItem.OrderItemId);

        await _context.SaveChangesAsync();
        InvalidateCache(orderId);

        var savedOrder = await LoadOrderAsync(orderId);
        if (savedOrder is null)
        {
            return OrderServiceResult.Failure(
                OrderErrorCode.OrderNotFound,
                $"Order {orderId} could not be loaded after update.");
        }

        return OrderServiceResult.Successful(savedOrder.ToResponseDto());
    }

    public async Task<OrderServiceResult> UpdateOrderInfoAsync(int orderId, UpdateOrderDto dto)
    {
        try
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.InventoryItem)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order is null)
            {
                return OrderServiceResult.Failure(
                    OrderErrorCode.OrderNotFound,
                    $"Order {orderId} not found.");
            }

            order.UpdateCustomerName(dto.CustomerName);

            await _context.SaveChangesAsync();
            InvalidateCache(orderId);

            var savedOrder = await LoadOrderAsync(orderId);
            if (savedOrder is null)
            {
                return OrderServiceResult.Failure(
                    OrderErrorCode.OrderNotFound,
                    $"Order {orderId} could not be loaded after update.");
            }

            return OrderServiceResult.Successful(savedOrder.ToResponseDto());
        }
        catch (ArgumentException ex)
        {
            return OrderServiceResult.Failure(OrderErrorCode.ValidationError, ex.Message);
        }
    }

    public async Task<OrderServiceResult> AdjustItemQuantityAsync(int orderId, int inventoryItemId, int quantityChange)
    {
        try
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.InventoryItem)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order is null)
            {
                return OrderServiceResult.Failure(
                    OrderErrorCode.OrderNotFound,
                    $"Order {orderId} not found.");
            }

            var orderItem = order.OrderItems.FirstOrDefault(i => i.InventoryItemId == inventoryItemId);
            if (orderItem is null)
            {
                return OrderServiceResult.Failure(
                    OrderErrorCode.OrderItemNotFound,
                    $"Order item with inventory item {inventoryItemId} not found.");
            }

            if (quantityChange == 0)
            {
                var unchangedOrder = await LoadOrderAsync(orderId);
                if (unchangedOrder is null)
                {
                    return OrderServiceResult.Failure(
                        OrderErrorCode.OrderNotFound,
                        $"Order {orderId} could not be loaded.");
                }

                return OrderServiceResult.Successful(unchangedOrder.ToResponseDto());
            }

            if (quantityChange > 0)
            {
                if (orderItem.InventoryItem is null)
                {
                    return OrderServiceResult.Failure(
                        OrderErrorCode.InventoryItemNotFound,
                        $"Inventory item {inventoryItemId} not found.");
                }

                if (!orderItem.InventoryItem.DecreaseStock(quantityChange))
                {
                    return OrderServiceResult.Failure(
                        OrderErrorCode.InsufficientStock,
                        $"Not enough stock for item {inventoryItemId}.");
                }

                orderItem.UpdateQuantity(orderItem.QuantityOrdered + quantityChange);
            }
            else
            {
                var decreaseAmount = Math.Abs(quantityChange);

                if (decreaseAmount >= orderItem.QuantityOrdered)
                {
                    orderItem.InventoryItem?.IncreaseStock(orderItem.QuantityOrdered);
                    order.RemoveItem(orderItem.OrderItemId);
                }
                else
                {
                    orderItem.InventoryItem?.IncreaseStock(decreaseAmount);
                    orderItem.UpdateQuantity(orderItem.QuantityOrdered - decreaseAmount);
                }
            }

            await _context.SaveChangesAsync();
            InvalidateCache(orderId);

            var savedOrder = await LoadOrderAsync(orderId);
            if (savedOrder is null)
            {
                return OrderServiceResult.Failure(
                    OrderErrorCode.OrderNotFound,
                    $"Order {orderId} could not be loaded after update.");
            }

            return OrderServiceResult.Successful(savedOrder.ToResponseDto());
        }
        catch (ArgumentException ex)
        {
            return OrderServiceResult.Failure(OrderErrorCode.ValidationError, ex.Message);
        }
    }

    public async Task<bool> DeleteAsync(int orderId)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.InventoryItem)
            .FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (order is null)
            return false;

        foreach (var item in order.OrderItems)
        {
            item.InventoryItem?.IncreaseStock(item.QuantityOrdered);
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        InvalidateCache(orderId);
        return true;
    }

    private async Task<Order?> LoadOrderAsync(int orderId)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.InventoryItem)
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.OrderId == orderId);
    }

    private void InvalidateCache(int? orderId = null)
    {
        _cache.Remove(CacheKeys.OrdersAll);

        if (orderId.HasValue)
            _cache.Remove(CacheKeys.OrderById(orderId.Value));
    }
}