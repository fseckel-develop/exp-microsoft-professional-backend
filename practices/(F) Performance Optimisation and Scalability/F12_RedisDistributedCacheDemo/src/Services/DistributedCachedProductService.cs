using Microsoft.Extensions.Caching.Distributed;
using RedisDistributedCacheDemo.Caching;
using RedisDistributedCacheDemo.Models;
using RedisDistributedCacheDemo.Services;

namespace RedisDistributedCacheDemo.Data;

public sealed class DistributedCachedProductService : IProductService
{
    // Note:
    // Unlike the in-memory demo, this distributed cache example does not implement
    // cross-instance stampede protection. In a multi-node production environment,
    // you would typically use a distributed lock or another coordination strategy.
    private readonly IDistributedCache _cache;
    private readonly IProductService _inner;

    public DistributedCachedProductService(IDistributedCache cache, IProductService inner)
    {
        _cache = cache;
        _inner = inner;
    }

    public async Task<Product?> GetProductAsync(Guid id, CancellationToken token = default)
    {
        var key = CacheKeys.ProductById(id);

        var cached = await _cache.GetRecordAsync<Product>(key, token);
        if (cached is not null)
        {
            Console.WriteLine($"[CACHE HIT] {key}");
            return cached;
        }

        Console.WriteLine($"[CACHE MISS] {key}");

        var product = await _inner.GetProductAsync(id, token);

        if (product is not null)
        {
            await _cache.SetRecordAsync(key, product, CacheOptions.ShortLived(), token);
            Console.WriteLine($"[CACHE SET] {key}");
        }

        return product;
    }

    public async Task<IReadOnlyList<Product>> GetFeaturedProductsAsync(CancellationToken token = default)
    {
        var key = CacheKeys.FeaturedProducts;

        var cached = await _cache.GetRecordAsync<List<Product>>(key, token);
        if (cached is not null)
        {
            Console.WriteLine($"[CACHE HIT] {key}");
            return cached;
        }

        Console.WriteLine($"[CACHE MISS] {key}");

        var featured = await _inner.GetFeaturedProductsAsync(token);

        await _cache.SetRecordAsync(key, featured.ToList(), CacheOptions.ShortLived(), token);
        Console.WriteLine($"[CACHE SET] {key}");

        return featured;
    }

    public async Task InvalidateProductAsync(Guid id, CancellationToken ct)
    {
        var key = CacheKeys.ProductById(id);
        await _cache.RemoveAsync(key, ct);
        Console.WriteLine($"[CACHE REMOVE] {key}");

        await InvalidateFeaturedAsync(ct);
    }

    public async Task InvalidateFeaturedAsync(CancellationToken ct = default)
    {
        await _cache.RemoveAsync(CacheKeys.FeaturedProducts, ct);
        Console.WriteLine($"[CACHE REMOVE] {CacheKeys.FeaturedProducts}");
    }
}