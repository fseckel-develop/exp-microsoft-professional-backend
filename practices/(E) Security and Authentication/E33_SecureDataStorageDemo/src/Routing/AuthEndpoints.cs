using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SecureDataStorageDemo.Contracts;
using SecureDataStorageDemo.Infrastructure;

namespace SecureDataStorageDemo.Routing;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth");

        // Dev-only token minting (so token generation is not hardcoded in Program)
        group.MapPost("/dev-token", (DevTokenRequestDto dto, IOptions<JwtOptions> opt) =>
        {
            var jwt = opt.Value;

            var keyBytes = Convert.FromBase64String(jwt.SigningKeyBase64);
            var creds = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256);

            var handler = new JwtSecurityTokenHandler();

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = jwt.Issuer,
                Audience = jwt.Audience,
                Expires = DateTime.UtcNow.AddMinutes(jwt.DevTokenMinutes),
                SigningCredentials = creds,
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, dto.UserId),
                    new Claim(JwtRegisteredClaimNames.Sub, dto.UserId),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                })
            };

            var token = handler.CreateToken(descriptor);
            var raw = handler.WriteToken(token);

            return Results.Ok(new { accessToken = raw, tokenType = "Bearer" });
        })
        .AllowAnonymous();

        group.MapGet("/whoami", (HttpContext ctx) =>
        {
            var u = ctx.User;
            return Results.Ok(new
            {
                authenticated = u.Identity?.IsAuthenticated ?? false,
                name = u.Identity?.Name,
                claims = u.Claims.Select(c => new { c.Type, c.Value })
            });
        }).RequireAuthorization();

        return app;
    }
}