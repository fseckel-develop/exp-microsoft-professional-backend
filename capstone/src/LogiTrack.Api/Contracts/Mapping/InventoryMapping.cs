using LogiTrack.Api.Contracts.Inventory;
using LogiTrack.Api.Models;

namespace LogiTrack.Api.Contracts.Mapping;

public static class InventoryMappings
{
    public static InventoryItemResponseDto ToResponseDto(this InventoryItem item)
    {
        return new InventoryItemResponseDto
        {
            InventoryItemId = item.ItemId,
            Name = item.Name,
            QuantityInStock = item.QuantityInStock,
            Location = item.Location,
            Price = item.Price
        };
    }
}