using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace RedisDistributedCacheDemo.Caching;

public static class CacheJson
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public static async Task SetRecordAsync<T>(
        this IDistributedCache cache,
        string key,
        T value,
        DistributedCacheEntryOptions options,
        CancellationToken ct = default)
    {
        byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(value, JsonOptions);
        await cache.SetAsync(key, bytes, options, ct);
    }

    public static async Task<T?> GetRecordAsync<T>(
        this IDistributedCache cache,
        string key,
        CancellationToken ct = default)
    {
        byte[]? bytes = await cache.GetAsync(key, ct);
        if (bytes is null)
            return default;

        return JsonSerializer.Deserialize<T>(bytes, JsonOptions);
    }
}