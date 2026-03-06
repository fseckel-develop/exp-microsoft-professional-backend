using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCacheDemo.Controllers;

[ApiController]
[Route("api/demo-cache")]
public class DemoCacheController : ControllerBase
{
    private readonly IMemoryCache _cache;

    public DemoCacheController(IMemoryCache cache)
    {
        _cache = cache;
    }

    [HttpPost("store")]
    public IActionResult Store()
    {
        string key = "product1";
        string value = "Product: EcoBottle, Price: $15";

        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            Size = 1
        };

        _cache.Set(key, value, options);

        return Ok("Data stored in cache successfully.");
    }

    [HttpGet("retrieve")]
    public IActionResult Retrieve()
    {
        string key = "product1";

        if (_cache.TryGetValue(key, out string? value))
        {
            return Ok($"Retrieved value: {value}");
        }

        return NotFound("The data was not found in the cache.");
    }

    [HttpDelete("remove")]
    public IActionResult Remove()
    {
        string key = "product1";

        if (!_cache.TryGetValue(key, out _))
        {
            return NotFound($"Key '{key}' not found in cache.");
        }

        _cache.Remove(key);

        return Ok($"Key '{key}' removed from cache.");
    }

    [HttpGet("monitor")]
    public IActionResult Monitor()
    {
        string key = "product1";

        bool hit = _cache.TryGetValue(key, out _);

        return hit
            ? Ok("Cache hit.")
            : NotFound("Cache miss.");
    }
}