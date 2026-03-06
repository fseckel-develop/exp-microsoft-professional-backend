using JwtAuthenticationDemo.Services;
using JwtAuthenticationDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace JwtAuthenticationDemo.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserStorage _users;
    private readonly IJwtTokenService _tokens;

    public AuthController(IUserStorage users, IJwtTokenService tokens)
    {
        _users = users;
        _tokens = tokens;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public ActionResult<LoginResponseDto> Login([FromBody] LoginRequestDto request)
    {
        if (!_users.ValidateCredentials(request.Username, request.Password))
            return Unauthorized("Invalid credentials.");

        var token = _tokens.CreateToken(request.Username);

        return Ok(new LoginResponseDto { Token = token });
    }

    // Helpful for debugging: shows what the API thinks you are
    [HttpGet("whoami")]
    [Authorize]
    public IActionResult WhoAmI()
    {
        var sub = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value
                  ?? User.Identity?.Name
                  ?? "(unknown)";

        return Ok(new
        {
            subject = sub,
            claims = User.Claims.Select(c => new { c.Type, c.Value })
        });
    }
}