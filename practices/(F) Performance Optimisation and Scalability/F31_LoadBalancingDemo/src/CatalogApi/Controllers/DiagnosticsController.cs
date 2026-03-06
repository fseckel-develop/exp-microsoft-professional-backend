using Microsoft.AspNetCore.Mvc;
using CatalogApi.Infrastructure;

namespace CatalogApi.Controllers;

[ApiController]
[Route("api/diagnostics")]
public sealed class DiagnosticsController : ControllerBase
{
    private readonly InstanceContext _instance;

    public DiagnosticsController(InstanceContext instance)
    {
        _instance = instance;
    }

    [HttpGet("request-info")]
    public IActionResult RequestInfo()
    {
        return Ok(new
        {
            instance = _instance.InstanceName,
            method = Request.Method,
            path = Request.Path.Value,
            host = Request.Host.Value,
            headers = new
            {
                forwardedFor = Request.Headers["X-Forwarded-For"].ToString(),
                forwardedHost = Request.Headers["X-Forwarded-Host"].ToString(),
                forwardedProto = Request.Headers["X-Forwarded-Proto"].ToString()
            },
            servedAtUtc = _instance.UtcNow
        });
    }

    [HttpGet("delay/{ms:int}")]
    public async Task<IActionResult> Delay(int ms, CancellationToken ct)
    {
        await Task.Delay(ms, ct);

        return Ok(new
        {
            instance = _instance.InstanceName,
            delayedByMs = ms,
            servedAtUtc = _instance.UtcNow
        });
    }
}