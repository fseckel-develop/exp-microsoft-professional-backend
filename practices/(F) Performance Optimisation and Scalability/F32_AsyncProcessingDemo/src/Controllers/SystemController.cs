using Microsoft.AspNetCore.Mvc;

namespace AsyncProcessingDemo.Controllers;

[ApiController]
[Route("")]
public sealed class SystemController : ControllerBase
{
    [HttpGet]
    public IActionResult Root()
    {
        return Ok(new
        {
            message = "Order Processing API Demo is running",
            servedAtUtc = DateTime.UtcNow
        });
    }

    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok(new
        {
            status = "Healthy",
            servedAtUtc = DateTime.UtcNow
        });
    }
}