using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace RedisDistributedCacheDemo.Controllers;

[ApiController]
[Route("api/demo-cache")]
public class DemoCacheController : ControllerBase
{
    private readonly IDistributedCache _cache;

    public DemoCacheController(IDistributedCache cache)
    {
        _cache = cache;
    }

    [HttpPost("store")]
    public async Task<IActionResult> Store(CancellationToken ct)
    {
        string key = "product1";
        string value = "Product: EcoBottle, Price: $15";

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        };

        await _cache.SetStringAsync(key, value, options, ct);

        return Ok("Data stored in Redis successfully.");
    }

    [HttpGet("retrieve")]
    public async Task<IActionResult> Retrieve(CancellationToken ct)
    {
        string key = "product1";

        string? value = await _cache.GetStringAsync(key, ct);

        if (value is not null)
        {
            return Ok($"Retrieved value: {value}");
        }

        return NotFound("The data was not found in the cache.");
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> Remove(CancellationToken ct)
    {
        string key = "product1";

        string? value = await _cache.GetStringAsync(key, ct);
        if (value is null)
        {
            return NotFound($"Key '{key}' not found in Redis.");
        }

        await _cache.RemoveAsync(key, ct);

        return Ok($"Key '{key}' removed from Redis.");
    }

    [HttpGet("monitor")]
    public async Task<IActionResult> Monitor(CancellationToken ct)
    {
        string key = "product1";

        string? value = await _cache.GetStringAsync(key, ct);

        return value is not null
            ? Ok("Cache hit.")
            : NotFound("Cache miss.");
    }
}