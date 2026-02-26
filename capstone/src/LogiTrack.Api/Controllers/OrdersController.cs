using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

using LogiTrack.Api.Entities;
using LogiTrack.Api.DTOs;

namespace LogiTrack.Api.Controllers;

[ApiController][Authorize]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly LogiTrackContext _context;

    public OrdersController(LogiTrackContext context) => _context = context;

    // GET: /api/orders
    [HttpGet][Authorize]
    public async Task<ActionResult<IEnumerable<ResponseOrderDto>>> GetAll()
    {
        var orders = await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.InventoryItem)
            .AsNoTracking()
            .Select(o => new ResponseOrderDto
            {
                OrderId = o.OrderId,
                CustomerName = o.CustomerName,
                DatePlaced = o.DatePlaced,
                TotalAmount = o.TotalAmount,
                OrderItems = o.OrderItems.Select(oi => new ResponseOrderItemDto
                {
                    OrderItemId = oi.OrderItemId,
                    InventoryItemId = oi.InventoryItemId,
                    InventoryItemName = oi.InventoryItem != null ? oi.InventoryItem.Name : string.Empty,
                    QuantityOrdered = oi.QuantityOrdered,
                    UnitPrice = oi.UnitPrice,
                    SubTotal = oi.SubTotal
                }).ToList()
            })
            .ToListAsync();

        return Ok(orders);
    }

    // GET: /api/orders/{id}
    [HttpGet("{id:int}")][Authorize]
    public async Task<ActionResult<ResponseOrderDto>> GetById(int id)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.InventoryItem)
            .AsNoTracking()
            .Where(o => o.OrderId == id)
            .Select(o => new ResponseOrderDto
            {
                OrderId = o.OrderId,
                CustomerName = o.CustomerName,
                DatePlaced = o.DatePlaced,
                TotalAmount = o.TotalAmount,
                OrderItems = o.OrderItems.Select(oi => new ResponseOrderItemDto
                {
                    OrderItemId = oi.OrderItemId,
                    InventoryItemId = oi.InventoryItemId,
                    InventoryItemName = oi.InventoryItem != null ? oi.InventoryItem.Name : string.Empty,
                    QuantityOrdered = oi.QuantityOrdered,
                    UnitPrice = oi.UnitPrice,
                    SubTotal = oi.SubTotal
                }).ToList()
            })
            .FirstOrDefaultAsync();

        return order == null ? NotFound() : Ok(order);
    }

    // POST: /api/orders
    [HttpPost][Authorize(Policy = "OrderWrite")]
    public async Task<ActionResult<ResponseOrderDto>> Create(CreateOrderDto dto)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);

        var order = new Order { CustomerName = dto.CustomerName };

        foreach (var itemDto in dto.OrderItems)
        {
            var inventory = await _context.InventoryItems.FindAsync(itemDto.InventoryItemId);
            if (inventory == null)
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

        return await GetById(order.OrderId);
    }

    // POST: /api/orders/{orderId}/items
    [HttpPost("{orderId:int}/items")][Authorize(Policy = "OrderWrite")]
    public async Task<ActionResult<ResponseOrderDto>> AddItemToOrder(int orderId, CreateOrderItemDto dto)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
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
        return await GetById(orderId);
    }

    // DELETE: /api/orders/{orderId}/items/{inventoryItemId}
    [HttpDelete("{id:int}/items/{inventoryItemId:int}")][Authorize(Policy = "OrderWrite")]
    public async Task<ActionResult<ResponseOrderDto>> RemoveItemFromOrder(int id, int inventoryItemId)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.OrderId == id);
        if (order == null) 
            return NotFound($"Order {id} not found.");

        var orderItem = order.OrderItems.FirstOrDefault(i => i.InventoryItemId == inventoryItemId);
        if (orderItem == null) 
            return NotFound($"OrderItem with InventoryItem {inventoryItemId} not found.");

        order.RemoveItem(orderItem.OrderItemId);
        await _context.SaveChangesAsync();

        return await GetById(id);
    }

    // PATCH: /api/orders/{id}
    [HttpPatch("{id:int}")][Authorize(Policy = "OrderWrite")]
    public async Task<ActionResult<ResponseOrderDto>> UpdateOrderInfo(int id, UpdateOrderDto dto)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.OrderId == id);

        if (order == null) 
            return NotFound($"Order {id} not found.");

        order.CustomerName = dto.CustomerName;

        await _context.SaveChangesAsync();

        return await GetById(id);
    }

    // PATCH: /api/orders/{id}/items/{inventoryItemId}
    [HttpPatch("{id:int}/items/{inventoryItemId:int}")][Authorize(Policy = "OrderWrite")]
    public async Task<ActionResult<ResponseOrderDto>> AdjustItemQuantity(int id, int inventoryItemId, [FromQuery] int quantityChange)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.OrderId == id);

        if (order == null) 
            return NotFound($"Order {id} not found.");

        var orderItem = order.OrderItems.FirstOrDefault(i => i.InventoryItemId == inventoryItemId);
        if (orderItem == null) 
            return NotFound($"OrderItem with InventoryItem {inventoryItemId} not found.");

        orderItem.QuantityOrdered += quantityChange;

        if (orderItem.QuantityOrdered <= 0)
        {
            order.RemoveItem(orderItem.OrderItemId);
        }

        await _context.SaveChangesAsync();

        return await GetById(id);
    }

    // DELETE: /api/orders/{id}
    [HttpDelete("{id:int}")][Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Delete(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null) return NotFound();

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}