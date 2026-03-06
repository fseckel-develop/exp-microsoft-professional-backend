using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtBestPracticesDemo.Controllers;

[ApiController]
[Route("api/values")]
public class ValuesController : ControllerBase
{
    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public IActionResult Admin() => Ok("This is Admin data");

    [HttpGet("user")]
    [Authorize(Roles = "User")]
    new public IActionResult User() => Ok("This is User data");

    [HttpGet("secure")]
    [Authorize]
    public IActionResult Secure() => Ok(new { Message = "This is a secure endpoint." });
}