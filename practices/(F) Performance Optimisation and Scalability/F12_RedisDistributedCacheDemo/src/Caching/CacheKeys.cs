namespace RedisDistributedCacheDemo.Caching;

public static class CacheKeys
{
    public static string ProductById(Guid id) => $"catalog:product:{id}";
    public const string FeaturedProducts = "catalog:featured-products";
}