using Microsoft.AspNetCore.Mvc;

namespace LinearDataSupportTicketApi.Controllers;

[ApiController]
[Route("")]
public sealed class SystemController : ControllerBase
{
    [HttpGet]
    public IActionResult Root()
    {
        return Ok(new
        {
            message = "Support Desk Api is running, demonstrating Linear Data Structures",
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