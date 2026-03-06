using Microsoft.Extensions.Caching.Memory;
using InMemoryCacheDemo.Services;
using InMemoryCacheDemo.Models;
using InMemoryCacheDemo.Caching;

namespace InMemoryCacheDemo.Data;

public sealed class InMemoryCachedProductService : IProductService
{
    private readonly IMemoryCache _cache;
    private readonly IProductService _inner;
    private readonly CacheStampede _stampede;

    public InMemoryCachedProductService(IMemoryCache cache, IProductService inner, CacheStampede stampede)
    {
        _cache = cache;
        _inner = inner;
        _stampede = stampede;
    }

    public async Task<Product?> GetProductAsync(Guid id, CancellationToken token = default)
    {
        var key = CacheKeys.ProductById(id);

        if (_cache.TryGetValue(key, out Product? cached))
        {
            Console.WriteLine($"[CACHE HIT] {key}");
            return cached;
        }

        Console.WriteLine($"[CACHE MISS] {key}");

        // Prevent stampede: only one fetch per key at a time
        return await _stampede.RunAsync(key, async () =>
        {
            // Double-check after acquiring lock
            if (_cache.TryGetValue(key, out Product? cachedInside))
            {
                Console.WriteLine($"[CACHE HIT after lock] {key}");
                return cachedInside;
            }

            var product = await _inner.GetProductAsync(id, token);

            if (product is not null)
            {
                _cache.Set(key, product, CacheOptions.ShortLived());
                Console.WriteLine($"[CACHE SET] {key}");
            }

            return product;
        });
    }

    public async Task<IReadOnlyList<Product>> GetFeaturedProductsAsync(CancellationToken token = default)
    {
        var key = CacheKeys.FeaturedProducts;

        if (_cache.TryGetValue(key, out IReadOnlyList<Product>? cached))
        {
            Console.WriteLine($"[CACHE HIT] {key}");
            return cached!;
        }

        Console.WriteLine($"[CACHE MISS] {key}");

        return await _stampede.RunAsync(key, async () =>
        {
            if (_cache.TryGetValue(key, out IReadOnlyList<Product>? cachedInside))
                return cachedInside!;

            var featured = await _inner.GetFeaturedProductsAsync(token);
            _cache.Set(key, featured, CacheOptions.ShortLived());
            Console.WriteLine($"[CACHE SET] {key}");
            return featured;
        });
    }

    public Task InvalidateProductAsync(Guid id, CancellationToken ct)
    {
        var key = CacheKeys.ProductById(id);
        _cache.Remove(key);
        Console.WriteLine($"[CACHE REMOVE] {key}");

        // Often product updates affect list queries too
        return InvalidateFeaturedAsync(ct);
    }

    public Task InvalidateFeaturedAsync(CancellationToken ct = default)
    {
        _cache.Remove(CacheKeys.FeaturedProducts);
        Console.WriteLine($"[CACHE REMOVE] {CacheKeys.FeaturedProducts}");
        return Task.CompletedTask;
    }
}