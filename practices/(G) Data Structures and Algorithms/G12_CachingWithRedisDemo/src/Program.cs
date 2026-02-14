using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static async Task Main(string[] args)
    {
        // ================================
        // (1) Configure Redis
        // ================================
        var services = new ServiceCollection();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "localhost:6379";
            options.InstanceName = "DemoInstance";
        });

        var provider = services.BuildServiceProvider();
        var cache = provider.GetRequiredService<IDistributedCache>();

        Console.WriteLine("Redis cache setup complete.\n");

        // ================================
        // (2) Basic Cache Demo (Product List)
        // ================================
        await BasicCacheDemo(cache);

        // ================================
        // (3) Expiration Demo
        // ================================
        await ExpirationDemo(cache);

        // ================================
        // (4) Manual Invalidation Demo
        // ================================
        await ManualInvalidationDemo(cache);

        // ================================
        // (5) Shared Counter (Multi-instance Consistency)
        // ================================
        await SharedCounterDemo(cache);

        Console.WriteLine("\nDemo complete.");
    }


    // ----------------------------------------------------
    // Basic Cache Example
    // ----------------------------------------------------
    static async Task BasicCacheDemo(IDistributedCache cache)
    {
        Console.WriteLine("=== BASIC CACHE DEMO ===");

        string key = "ProductList";
        string? cachedData = await cache.GetStringAsync(key);

        if (cachedData != null)
        {
            Console.WriteLine("Cache Hit: " + cachedData);
        }
        else
        {
            Console.WriteLine("Cache Miss: Generating new data...");
            string productData = "Product1, Product2, Product3";

            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

            await cache.SetStringAsync(key, productData, options);

            Console.WriteLine("Data Stored in Cache: " + productData);
        }

        Console.WriteLine();
    }


    // ----------------------------------------------------
    // Expiration Policies Demo
    // ----------------------------------------------------
    static async Task ExpirationDemo(IDistributedCache cache)
    {
        Console.WriteLine("=== EXPIRATION POLICY DEMO ===");

        string key = "taskKey";

        var cacheEntryOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            SlidingExpiration = TimeSpan.FromMinutes(2)
        };

        await cache.SetStringAsync(key, "Sample Task", cacheEntryOptions);
        Console.WriteLine("Cache entry set with absolute and sliding expiration.");

        // Simulate repeated access
        for (int i = 0; i < 3; i++)
        {
            var value = await cache.GetStringAsync(key);

            if (value != null)
                Console.WriteLine($"Cache hit: {value}");
            else
                Console.WriteLine("Cache miss: Value expired.");

            await Task.Delay(TimeSpan.FromSeconds(5));
        }

        Console.WriteLine();
    }


    // ----------------------------------------------------
    // Manual Invalidation
    // ----------------------------------------------------
    static async Task ManualInvalidationDemo(IDistributedCache cache)
    {
        Console.WriteLine("=== MANUAL INVALIDATION DEMO ===");

        string key = "taskKey";

        Console.WriteLine("Press any key to invalidate cache...");
        Console.ReadKey();

        await cache.RemoveAsync(key);
        Console.WriteLine($"\nCache entry '{key}' has been invalidated.");

        var value = await cache.GetStringAsync(key);
        Console.WriteLine(value == null
            ? "Cache miss after invalidation."
            : "Cache still exists.");

        Console.WriteLine();
    }


    // ----------------------------------------------------
    // Shared Counter Demo (Multi-instance consistency)
    // ----------------------------------------------------
    static async Task SharedCounterDemo(IDistributedCache cache)
    {
        Console.WriteLine("=== SHARED COUNTER DEMO ===");

        string key = "SharedCounter";

        string? cachedValue = await cache!.GetStringAsync(key);
        int counter = cachedValue != null ? int.Parse(cachedValue) : 0;

        counter++;

        await cache.SetStringAsync(key, counter.ToString());

        Console.WriteLine($"Updated Counter: {counter}");
        Console.WriteLine("Run this program in multiple instances to observe distributed consistency.\n");
    }
}