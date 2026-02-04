using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("\n----------------------------\n");

        AbsoluteExpiration();

        Console.WriteLine("\n----------------------------\n");

        SlidingExpiration();

        Console.WriteLine("\n----------------------------\n");

        await DependentExpiration();

        Console.WriteLine("\n----------------------------\n");
    }


    // Absolute Expiration using IMemoryCache
    static void AbsoluteExpiration()
    {
        var cache = new MemoryCache(new MemoryCacheOptions());

        string sessionKey = "absolute-session";
        string sessionData = "User session data";

        var options = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(10));

        cache.Set(sessionKey, sessionData, options);
        Console.WriteLine("Session stored with absolute expiration.");

        for (int i = 0; i < 2; i++)
        {
            if (cache.TryGetValue(sessionKey, out string cachedValue))
            {
                Console.WriteLine($"Cached Value: {cachedValue}");
            }
            else
            {
                Console.WriteLine("Session expired (absolute expiration).");
            }
            Thread.Sleep(5500);
        }
    }


    // Sliding Expiration using IMemoryCache
    static void SlidingExpiration()
    {
        var cache = new MemoryCache(new MemoryCacheOptions());

        string sessionKey = "sliding-session";
        string sessionData = "Sliding session data";

        var options = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromSeconds(5));

        cache.Set(sessionKey, sessionData, options);
        Console.WriteLine("Session stored with sliding expiration.");

        for (int i = 0; i < 3; i++)
        {
            Thread.Sleep(3000);

            if (cache.TryGetValue(sessionKey, out string cachedValue))
            {
                Console.WriteLine($"Access {i + 1}: Cached Value: {cachedValue}");
            }
            else
            {
                Console.WriteLine("Session expired.");
                return;
            }
        }

        // Allow expiration after no access
        Thread.Sleep(6000);

        if (!cache.TryGetValue(sessionKey, out _))
        {
            Console.WriteLine("Session expired after sliding window elapsed.");
        }
    }


    // Dependent Expiration using Redis
    static async Task DependentExpiration()
    {
        var redis = ConnectionMultiplexer.Connect("localhost");
        var db = redis.GetDatabase();

        string sessionKey = "session-key";
        string historyKey = "login-history";

        await db.StringSetAsync(sessionKey, "Session Active");
        await db.StringSetAsync(historyKey, "Login at " + DateTime.Now);

        Console.WriteLine("Session and login history stored in Redis.");

        // Set expiration on the parent key
        var duration = TimeSpan.FromSeconds(10);
        await db.KeyExpireAsync(sessionKey, duration);
        Console.WriteLine("Session key set to expire in {0} seconds.", duration.TotalSeconds);

        // Simulate time passing
        await Task.Delay(11000);

        // Check dependency
        bool sessionExists = await db.KeyExistsAsync(sessionKey);

        if (!sessionExists)
        {
            await db.KeyDeleteAsync(historyKey);
            Console.WriteLine("Session expired. Login history deleted.");
        }
        else
        {
            Console.WriteLine("Session still active.");
        }
    }
}