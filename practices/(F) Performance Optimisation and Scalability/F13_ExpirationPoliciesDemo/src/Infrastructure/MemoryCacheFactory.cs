using Microsoft.Extensions.Caching.Memory;

namespace ExpirationPoliciesDemo.Infrastructure;

public static class MemoryCacheFactory
{
    public static IMemoryCache Create() => new MemoryCache(new MemoryCacheOptions());
}