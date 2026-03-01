namespace LogiTrack.Api.Services.Caching;

public static class CacheKeys
{
    public const string OrdersAll = "orders_all";
    public const string InventoryAll = "inventory_all";

    public static string OrderById(int orderId) => $"order_{orderId}";
    public static string InventoryById(int itemId) => $"inventory_{itemId}";
}