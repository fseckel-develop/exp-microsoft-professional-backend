using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeVault.Web.Contracts;
using SafeVault.Web.Services;

namespace SafeVault.Web.Controllers;

[ApiController]
[Route("api/auth")]
[AllowAnonymous]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequestDto dto,
        CancellationToken ct)
    {
        if (await _authService.UserExistsAsync(dto.Username, ct))
            return Conflict(new { error = "Username already exists." });

        await _authService.RegisterAsync(dto, ct);
        return Ok(new { message = "User registered successfully." });
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(
        [FromBody] LoginRequestDto dto,
        CancellationToken ct)
    {
        var result = await _authService.LoginAsync(dto, ct);

        if (result is null)
            return Unauthorized(new { error = "Invalid username or password." });

        return Ok(result);
    }
}