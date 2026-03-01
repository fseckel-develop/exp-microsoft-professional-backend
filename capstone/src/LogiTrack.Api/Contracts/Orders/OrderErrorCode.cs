namespace LogiTrack.Api.Contracts.Orders;

public enum OrderErrorCode
{
    None = 0,
    OrderNotFound = 1,
    InventoryItemNotFound = 2,
    OrderItemNotFound = 3,
    InsufficientStock = 4,
    ValidationError = 5
}