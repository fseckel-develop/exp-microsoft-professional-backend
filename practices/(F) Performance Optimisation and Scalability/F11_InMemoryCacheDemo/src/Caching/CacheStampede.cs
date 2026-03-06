using System.Collections.Concurrent;

namespace InMemoryCacheDemo.Caching;

// Prevents cache stampedes by serializing factory execution per cache key.
public sealed class CacheStampede
{
    private readonly ConcurrentDictionary<string, SemaphoreSlim> _locks = new();

    public async Task<T> RunAsync<T>(string key, Func<Task<T>> factory)
    {
        var gate = _locks.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));

        await gate.WaitAsync();
        try
        {
            return await factory();
        }
        finally
        {
            gate.Release();

            // Optional cleanup: remove unused locks (best-effort)
            if (gate.CurrentCount == 1)
                _locks.TryRemove(key, out _);
        }
    }
}