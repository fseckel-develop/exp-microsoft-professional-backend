using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

using LogiTrack.Api.Entities;
using LogiTrack.Api.DTOs;

namespace LogiTrack.Api.Controllers;

[ApiController][Authorize]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly LogiTrackContext _context;
    private readonly IMemoryCache _cache;
    private const string OrdersCacheKey = "orders_all";

    private static readonly Expression<Func<Order, ResponseOrderDto>> OrderSelector =
        order => new ResponseOrderDto
        {
            OrderId = order.OrderId,
            CustomerName = order.CustomerName,
            DatePlaced = order.DatePlaced,
            TotalAmount = order.TotalAmount,
            OrderItems = order.OrderItems.Select(oi => new ResponseOrderItemDto
            {
                OrderItemId = oi.OrderItemId,
                InventoryItemId = oi.InventoryItemId,
                InventoryItemName = oi.InventoryItem != null ? oi.InventoryItem.Name : string.Empty,
                QuantityOrdered = oi.QuantityOrdered,
                UnitPrice = oi.UnitPrice,
                SubTotal = oi.SubTotal
            }).ToList()
        };


    public OrdersController(LogiTrackContext context, IMemoryCache cache) 
    {
        _context = context;
        _cache = cache;
    }

    // GET: /api/orders
    [HttpGet][Authorize]
    public async Task<ActionResult<IEnumerable<ResponseOrderDto>>> GetAll()
    {
        if (!_cache.TryGetValue(OrdersCacheKey, out List<ResponseOrderDto>? orders))
        {
            orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.InventoryItem)
                .AsNoTracking()
                .Select(OrderSelector)
                .ToListAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));

            _cache.Set(OrdersCacheKey, orders, cacheEntryOptions);
        }

        return Ok(orders);
    }

    // GET: /api/orders/{id}
    [HttpGet("{orderId:int}")][Authorize]
    public async Task<ActionResult<ResponseOrderDto>> GetById(int orderId)
    {
        string cacheKey = $"order_{orderId}";
        if (!_cache.TryGetValue(cacheKey, out ResponseOrderDto? order))
        {
            order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.InventoryItem)
                .AsNoTracking()
                .Where(o => o.OrderId == orderId)
                .Select(OrderSelector)
                .FirstOrDefaultAsync();

            if (order != null)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _cache.Set(cacheKey, order, cacheEntryOptions);
            }
        }

        return order == null ? NotFound($"Order {orderId} not found.") : Ok(order);
    }

    // POST: /api/orders
    [HttpPost][Authorize(Policy = "OrderWrite")]
    public async Task<ActionResult<ResponseOrderDto>> Create(CreateOrderDto dto)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);

        var inventoryIds = dto.OrderItems.Select(i => i.InventoryItemId).ToList();
        var inventoryMap = await _context.InventoryItems
            .Where(i => inventoryIds.Contains(i.ItemId))
            .ToDictionaryAsync(i => i.ItemId);

        var order = new Order { CustomerName = dto.CustomerName };

        foreach (var itemDto in dto.OrderItems)
        {
            if (!inventoryMap.TryGetValue(itemDto.InventoryItemId, out var inventory)) 
                return BadRequest($"Inventory item {itemDto.InventoryItemId} does not exist.");

            order.AddItem(new OrderItem
            {
                InventoryItemId = itemDto.InventoryItemId,
                QuantityOrdered = itemDto.QuantityOrdered,
                UnitPrice = inventory.Price
            });
        }

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        InvalidateCache();

        var responseDto = OrderSelector.Compile().Invoke(order);

        return CreatedAtAction(nameof(GetById), new { orderId = order.OrderId }, responseDto);
    }

    // POST: /api/orders/{orderId}/items
    [HttpPost("{orderId:int}/items")][Authorize(Policy = "OrderWrite")]
    public async Task<ActionResult<ResponseOrderDto>> AddItemToOrder(int orderId, CreateOrderItemDto dto)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .AsTracking()
            .FirstOrDefaultAsync(o => o.OrderId == orderId);
        if (order == null) 
            return NotFound($"Order {orderId} not found.");

        var inventory = await _context.InventoryItems.FindAsync(dto.InventoryItemId);
        if (inventory == null) 
            return BadRequest($"InventoryItem {dto.InventoryItemId} does not exist.");

        var existingItem = order.OrderItems.FirstOrDefault(i => i.InventoryItemId == dto.InventoryItemId);
        if (existingItem != null)
        {
            existingItem.QuantityOrdered += dto.QuantityOrdered;
        }
        else
        {
            order.AddItem(new OrderItem
            {
                InventoryItemId = dto.InventoryItemId,
                QuantityOrdered = dto.QuantityOrdered,
                UnitPrice = inventory.Price
            });
        }

        await _context.SaveChangesAsync();

        InvalidateCache(orderId);

        return await GetById(orderId);
    }

    // DELETE: /api/orders/{orderId}/items/{inventoryItemId}
    [HttpDelete("{orderId:int}/items/{inventoryItemId:int}")][Authorize(Policy = "OrderWrite")]
    public async Task<ActionResult<ResponseOrderDto>> RemoveItemFromOrder(int orderId, int inventoryItemId)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .AsTracking()
            .FirstOrDefaultAsync(o => o.OrderId == orderId);
        if (order == null) 
            return NotFound($"Order {orderId} not found.");

        var orderItem = order.OrderItems.FirstOrDefault(i => i.InventoryItemId == inventoryItemId);
        if (orderItem == null) 
            return NotFound($"OrderItem with InventoryItem {inventoryItemId} not found.");

        order.RemoveItem(orderItem.OrderItemId);
        await _context.SaveChangesAsync();

        InvalidateCache(orderId);

        return await GetById(orderId);
    }

    // PATCH: /api/orders/{id}
    [HttpPatch("{orderId:int}")][Authorize(Policy = "OrderWrite")]
    public async Task<ActionResult<ResponseOrderDto>> UpdateOrderInfo(int orderId, UpdateOrderDto dto)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .AsTracking()
            .FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (order == null) 
            return NotFound($"Order {orderId} not found.");

        order.CustomerName = dto.CustomerName;

        await _context.SaveChangesAsync();

        InvalidateCache(orderId);

        return await GetById(orderId);
    }

    // PATCH: /api/orders/{id}/items/{inventoryItemId}
    [HttpPatch("{orderId:int}/items/{inventoryItemId:int}")][Authorize(Policy = "OrderWrite")]
    public async Task<ActionResult<ResponseOrderDto>> AdjustItemQuantity(int orderId, int inventoryItemId, [FromQuery] int quantityChange)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .AsTracking()
            .FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (order == null) 
            return NotFound($"Order {orderId} not found.");

        var orderItem = order.OrderItems.FirstOrDefault(i => i.InventoryItemId == inventoryItemId);
        if (orderItem == null) 
            return NotFound($"OrderItem with InventoryItem {inventoryItemId} not found.");

        orderItem.QuantityOrdered += quantityChange;

        if (orderItem.QuantityOrdered <= 0)
        {
            order.RemoveItem(orderItem.OrderItemId);
        }

        await _context.SaveChangesAsync();

        InvalidateCache(orderId);

        return await GetById(orderId);
    }

    // DELETE: /api/orders/{id}
    [HttpDelete("{orderId:int}")][Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Delete(int orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null) 
            return NotFound($"Order {orderId} not found.");

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        InvalidateCache(orderId);

        return NoContent();
    }

    private void InvalidateCache(int? orderId = null)
    {
        _cache.Remove(OrdersCacheKey);
        if (orderId.HasValue) _cache.Remove($"order_{orderId.Value}");
    }
}