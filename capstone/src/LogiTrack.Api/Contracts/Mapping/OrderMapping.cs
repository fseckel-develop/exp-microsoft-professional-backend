using LogiTrack.Api.Contracts.Orders;
using LogiTrack.Api.Models;

namespace LogiTrack.Api.Contracts.Mapping;

public static class OrderMappings
{
    public static OrderResponseDto ToResponseDto(this Order order)
    {
        return new OrderResponseDto
        {
            OrderId = order.OrderId,
            CustomerName = order.CustomerName,
            DatePlaced = order.DatePlaced,
            TotalAmount = order.TotalAmount,
            OrderItems = order.OrderItems.Select(oi => new OrderItemResponseDto
            {
                OrderItemId = oi.OrderItemId,
                InventoryItemId = oi.InventoryItemId,
                InventoryItemName = oi.InventoryItem?.Name ?? string.Empty,
                QuantityOrdered = oi.QuantityOrdered,
                UnitPrice = oi.UnitPrice,
                SubTotal = oi.SubTotal
            }).ToList()
        };
    }
}