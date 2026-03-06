using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCacheDemo.Caching;

public static class CacheOptions
{
    public static MemoryCacheEntryOptions ShortLived() => new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
        SlidingExpiration = TimeSpan.FromMinutes(2),
        Priority = CacheItemPriority.Normal,
        Size = 1
    };
}