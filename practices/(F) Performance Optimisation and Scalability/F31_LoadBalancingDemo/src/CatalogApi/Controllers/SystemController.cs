using Microsoft.AspNetCore.Mvc;
using CatalogApi.Infrastructure;

namespace CatalogApi.Controllers;

[ApiController]
[Route("")]
public sealed class SystemController : ControllerBase
{
    private readonly InstanceContext _instance;

    public SystemController(InstanceContext instance)
    {
        _instance = instance;
    }

    [HttpGet]
    public IActionResult Root()
    {
        return Ok(new
        {
            message = "Catalog API is running",
            instance = _instance.InstanceName,
            servedAtUtc = _instance.UtcNow
        });
    }

    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok(new
        {
            status = "Healthy",
            instance = _instance.InstanceName,
            servedAtUtc = _instance.UtcNow
        });
    }

    [HttpGet("api/instance")]
    public IActionResult Instance()
    {
        return Ok(new
        {
            instance = _instance.InstanceName,
            machine = _instance.MachineName,
            processId = _instance.ProcessId,
            servedAtUtc = _instance.UtcNow
        });
    }
}