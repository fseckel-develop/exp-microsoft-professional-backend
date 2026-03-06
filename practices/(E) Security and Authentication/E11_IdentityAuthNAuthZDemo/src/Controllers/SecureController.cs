using IdentityAuthNAuthZDemo.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAuthNAuthZDemo.Controllers;

[ApiController]
[Route("api/secure")]
public class SecureController : ControllerBase
{
    [HttpGet("public")]
    [AllowAnonymous]
    public IActionResult Public() => Ok("Public endpoint, no auth required");

    [HttpGet("admin-area")]
    [Authorize(Policy = AccessRules.Policies.AdminArea)]
    public IActionResult AdminArea() => Ok("Admin-only access granted");

    [HttpGet("it-only")]
    [Authorize(Policy = AccessRules.Policies.ITOnly)]
    public IActionResult ITOnly() => Ok("IT Department access granted");
}