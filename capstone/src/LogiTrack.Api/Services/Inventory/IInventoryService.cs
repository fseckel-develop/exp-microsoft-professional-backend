using LogiTrack.Api.Contracts.Inventory;

namespace LogiTrack.Api.Services.Inventory;

public interface IInventoryService
{
    Task<List<InventoryItemResponseDto>> GetAllAsync();
    Task<InventoryItemResponseDto?> GetByIdAsync(int itemId);
    Task<InventoryItemResponseDto> CreateAsync(CreateInventoryItemDto dto);
    Task<InventoryItemResponseDto?> UpdateAsync(int itemId, UpdateInventoryItemDto dto);
    Task<bool> DeleteAsync(int itemId);
}