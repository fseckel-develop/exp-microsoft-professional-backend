using LogiTrack.Api.Contracts.Auth;
using LogiTrack.Api.Models;
using LogiTrack.Api.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Swashbuckle.AspNetCore.Annotations;

namespace LogiTrack.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Tags("Authentication")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IEmailSender _emailSender;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IWebHostEnvironment _environment;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IEmailSender emailSender,
        IJwtTokenService jwtTokenService,
        IRefreshTokenService refreshTokenService,
        IWebHostEnvironment environment)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailSender = emailSender;
        _jwtTokenService = jwtTokenService;
        _refreshTokenService = refreshTokenService;
        _environment = environment;
    }

    [HttpPost("register")]
    [SwaggerOperation(
        Summary = "Register a new user",
        Description = "Creates a new user account and sends an email confirmation link. Newly registered users are not assigned a role automatically."
    )]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var confirmationLink = Url.Action(
            nameof(ConfirmEmail),
            "Auth",
            new { userId = user.Id, token },
            Request.Scheme);

        await _emailSender.SendEmailAsync(
            user.Email!,
            "Confirm your email",
            $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");

        return Ok("User registered successfully. Please check your email to confirm your account.");
    }

    [HttpPost("login")]
    [SwaggerOperation(
        Summary = "Log in a user",
        Description = "Authenticates a confirmed user, returns a JWT access token, and issues a refresh token cookie."
    )]
    [ProducesResponseType(typeof(AuthTokenResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthTokenResponseDto>> Login(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user is null)
            return Unauthorized("Invalid credentials.");

        if (!user.EmailConfirmed)
            return Unauthorized("Email not confirmed.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: true);

        if (!result.Succeeded)
            return Unauthorized("Invalid credentials.");

        var jwtToken = await _jwtTokenService.GenerateTokenAsync(user);
        var refreshToken = _refreshTokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _userManager.UpdateAsync(user);

        Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = !_environment.IsDevelopment(),
            SameSite = SameSiteMode.Strict,
            Expires = user.RefreshTokenExpiryTime
        });

        return Ok(new AuthTokenResponseDto
        {
            Token = jwtToken
        });
    }

    [HttpPost("logout")]
    [Authorize]
    [SwaggerOperation(
        Summary = "Log out the current user",
        Description = "Clears the stored refresh token, updates the user's security stamp, and deletes the refresh token cookie."
    )]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Logout()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized();

        var user = await _userManager.FindByIdAsync(userId);
        if (user is not null)
        {
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;
            await _userManager.UpdateAsync(user);
            await _userManager.UpdateSecurityStampAsync(user);
        }

        Response.Cookies.Delete("refreshToken");

        return Ok("Logged out successfully.");
    }

    [HttpGet("confirm-email")]
    [SwaggerOperation(
        Summary = "Confirm a user's email address",
        Description = "Confirms a user's email using the userId and token generated during registration."
    )]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return BadRequest("Invalid user ID.");

        var result = await _userManager.ConfirmEmailAsync(user, token);

        if (!result.Succeeded)
            return BadRequest("Email confirmation failed.");

        return Ok("Email confirmed successfully.");
    }

    [HttpPost("refresh-token")]
    [SwaggerOperation(
        Summary = "Refresh the access token",
        Description = "Uses the refresh token cookie to issue a new JWT access token and rotate the refresh token."
    )]
    [ProducesResponseType(typeof(AuthTokenResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthTokenResponseDto>> RefreshToken()
    {
        if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            return Unauthorized("No refresh token provided.");

        var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

        if (user is null || user.RefreshTokenExpiryTime is null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return Unauthorized("Invalid or expired refresh token.");

        var newJwtToken = await _jwtTokenService.GenerateTokenAsync(user);
        var newRefreshToken = _refreshTokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _userManager.UpdateAsync(user);

        Response.Cookies.Append("refreshToken", newRefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = !_environment.IsDevelopment(),
            SameSite = SameSiteMode.Strict,
            Expires = user.RefreshTokenExpiryTime
        });

        return Ok(new AuthTokenResponseDto
        {
            Token = newJwtToken
        });
    }

    [HttpGet("users")]
    [Authorize(Policy = "AdminOnly")]
    [SwaggerOperation(
        Summary = "Get all users",
        Description = "Returns all registered users. This endpoint is restricted to Admin users."
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userManager.Users
            .Select(u => new
            {
                u.Id,
                u.UserName,
                u.Email,
                u.EmailConfirmed,
                u.RefreshTokenExpiryTime
            })
            .ToListAsync();

        return Ok(users);
    }
}