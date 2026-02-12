namespace E1_roles_claims_controlled.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/protected")]
public class ProtectedController : ControllerBase
{
    [HttpGet("admin-only")]
    [Authorize(Roles = "Admin")]
    public IActionResult AdminOnly()
    {
        return Ok("Admin-only access granted");
    }

    [HttpGet("it-only")]
    [Authorize(Policy = "ITDepartment")]
    public IActionResult ITOnly()
    {
        return Ok("IT Department access granted");
    }

    [HttpGet("public")]
    public IActionResult Public()
    {
        return Ok("Public endpoint, no auth required");
    }
}