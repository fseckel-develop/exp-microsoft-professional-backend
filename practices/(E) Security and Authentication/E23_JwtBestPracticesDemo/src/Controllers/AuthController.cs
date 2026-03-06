using JwtBestPracticesDemo.Infrastructure;
using JwtBestPracticesDemo.Contracts;
using JwtBestPracticesDemo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

namespace JwtBestPracticesDemo.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserDirectory _users;
    private readonly IJwtIssuer _jwt;
    private readonly IRefreshTokenStore _refresh;
    private readonly JwtOptions _opt;

    public AuthController(
        IUserDirectory users,
        IJwtIssuer jwt,
        IRefreshTokenStore refresh,
        IOptions<JwtOptions> opt)
    {
        _users = users;
        _jwt = jwt;
        _refresh = refresh;
        _opt = opt.Value;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public ActionResult<TokenPairResponseDto> Login([FromBody] LoginRequestDto dto)
    {
        if (!_users.TryValidate(dto.Username, dto.Password, out var role))
            return Unauthorized("Invalid credentials.");

        var access = _jwt.CreateAccessToken(dto.Username, role);
        var refresh = _refresh.Issue(dto.Username, role, DateTimeOffset.UtcNow.AddMinutes(_opt.RefreshTokenMinutes));

        return Ok(new TokenPairResponseDto { AccessToken = access, RefreshToken = refresh });
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public ActionResult<TokenPairResponseDto> Refresh([FromBody] RefreshRequestDto dto)
    {
        if (!_refresh.TryConsume(dto.RefreshToken, out var identity))
            return Unauthorized("Invalid or expired refresh token.");

        // rotate refresh token (old already consumed)
        var access = _jwt.CreateAccessToken(identity.Username, identity.Role);
        var refresh = _refresh.Issue(identity.Username, identity.Role, DateTimeOffset.UtcNow.AddMinutes(_opt.RefreshTokenMinutes));

        return Ok(new TokenPairResponseDto { AccessToken = access, RefreshToken = refresh });
    }

    // Optional but super useful for debugging tokens/roles
    [HttpGet("whoami")]
    [Authorize]
    public IActionResult WhoAmI()
    {
        var sub = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value
                  ?? User.Identity?.Name
                  ?? "(unknown)";

        var roles = User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role)
                               .Select(c => c.Value)
                               .ToArray();

        return Ok(new
        {
            subject = sub,
            roles,
            claims = User.Claims.Select(c => new { c.Type, c.Value })
        });
    }
}