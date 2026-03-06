using System.Security.Claims;
using IdentityAuthNAuthZDemo.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAuthNAuthZDemo.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthApiController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signIn;
    private readonly UserManager<IdentityUser> _users;

    public AuthApiController(SignInManager<IdentityUser> signIn, UserManager<IdentityUser> users)
    {
        _signIn = signIn;
        _users = users;
    }

    // GET /api/auth/whoami
    // Useful to confirm cookie auth is actually being sent in your HTTP client
    [HttpGet("whoami")]
    public IActionResult WhoAmI()
    {
        var authed = User?.Identity?.IsAuthenticated ?? false;

        return Ok(new
        {
            authenticated = authed,
            name = authed ? (User!.Identity?.Name ?? "(unknown)") : null,
            roles = authed ? User!.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToArray() : Array.Empty<string>(),
            claims = authed ? User!.Claims.Select(c => new { c.Type, c.Value }).ToArray() : Array.Empty<object>()
        });
    }

    // POST /api/auth/login
    // Sets the Identity auth cookie without redirects (better than MVC form login for .http testing)
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] ApiLoginRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Email) || string.IsNullOrWhiteSpace(req.Password))
            return BadRequest("Email and password are required.");

        var email = req.Email.Trim();

        // Ensure user exists (optional, but gives clearer errors)
        var user = await _users.FindByEmailAsync(email);
        if (user is null)
            return Unauthorized("Invalid credentials.");

        var result = await _signIn.PasswordSignInAsync(
            userName: email,               // you use email as username
            password: req.Password,
            isPersistent: req.RememberMe,
            lockoutOnFailure: false);

        if (!result.Succeeded)
            return Unauthorized("Invalid credentials.");

        return Ok("Logged in (cookie issued).");
    }

    // POST /api/auth/logout
    // Clears cookie without antiforgery requirements (perfect for .http scripts)
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signIn.SignOutAsync();
        return Ok("Logged out.");
    }

    // POST /api/auth/claims
    // Adds a claim to the CURRENT logged-in user (self-service)
    // This enables your IT-only policy tests.
    [HttpPost("claims")]
    [Authorize] // must be logged in
    public async Task<IActionResult> AddClaim([FromBody] AddClaimRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Type) || string.IsNullOrWhiteSpace(req.Value))
            return BadRequest("Claim type and value are required.");

        var user = await _users.GetUserAsync(User);
        if (user is null)
            return Unauthorized("No current user.");

        var type = req.Type.Trim();
        var value = req.Value.Trim();

        var existing = await _users.GetClaimsAsync(user);
        if (existing.Any(c => c.Type == type && c.Value == value))
            return Ok("Claim already present.");

        var add = await _users.AddClaimAsync(user, new Claim(type, value));
        if (!add.Succeeded)
            return BadRequest(add.Errors.Select(e => e.Description));

        return Ok("Claim added.");
    }

    // Optional: convenience endpoint specifically for your course policy
    // POST /api/auth/make-me-it  => adds Department=IT
    [HttpPost("make-me-it")]
    [Authorize]
    public Task<IActionResult> MakeMeIT()
        => AddClaim(new AddClaimRequest { Type = AccessRules.Claims.Department, Value = AccessRules.Claims.IT });
}

public class ApiLoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool RememberMe { get; set; } = false;
}

public class AddClaimRequest
{
    public string Type { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}