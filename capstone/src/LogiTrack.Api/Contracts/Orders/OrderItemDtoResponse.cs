namespace LogiTrack.Api.Contracts.Orders;

public class OrderItemResponseDto
{
    public int OrderItemId { get; set; }
    public int InventoryItemId { get; set; }
    public string InventoryItemName { get; set; } = string.Empty;
    public int QuantityOrdered { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal SubTotal { get; set; }
}