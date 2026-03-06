using Microsoft.Extensions.Caching.Distributed;

namespace RedisDistributedCacheDemo.Caching;

public static class CacheOptions
{
    public static DistributedCacheEntryOptions ShortLived() => new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
        SlidingExpiration = TimeSpan.FromMinutes(2)
    };
}