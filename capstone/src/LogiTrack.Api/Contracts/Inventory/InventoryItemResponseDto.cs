namespace LogiTrack.Api.Contracts.Inventory;

public class InventoryItemResponseDto
{
    public int InventoryItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int QuantityInStock { get; set; }
    public string Location { get; set; } = string.Empty;
    public decimal Price { get; set; }
}