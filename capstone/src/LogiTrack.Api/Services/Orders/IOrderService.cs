using LogiTrack.Api.Contracts.Orders;

namespace LogiTrack.Api.Services.Orders;

public interface IOrderService
{
    Task<List<OrderResponseDto>> GetAllAsync();
    Task<OrderResponseDto?> GetByIdAsync(int orderId);

    Task<OrderServiceResult> CreateAsync(CreateOrderDto dto);
    Task<OrderServiceResult> AddItemAsync(int orderId, CreateOrderItemDto dto);
    Task<OrderServiceResult> RemoveItemAsync(int orderId, int inventoryItemId);
    Task<OrderServiceResult> UpdateOrderInfoAsync(int orderId, UpdateOrderDto dto);
    Task<OrderServiceResult> AdjustItemQuantityAsync(int orderId, int inventoryItemId, int quantityChange);

    Task<bool> DeleteAsync(int orderId);
}