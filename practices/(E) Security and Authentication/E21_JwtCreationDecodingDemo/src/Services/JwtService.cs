using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using JwtCreationDecodingDemo.Models;

namespace JwtCreationDecodingDemo.Services;

public sealed class JwtService
{
    private readonly JwtOptions _options;
    private readonly JwtSecurityTokenHandler _handler = new();

    public JwtService(JwtOptions options)
    {
        _options = options;
    }

    public string IssueToken(JwtSubject subject)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = BuildClaims(subject);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.Add(_options.Lifetime),
            signingCredentials: creds
        );

        return _handler.WriteToken(token);
    }

    public JwtValidationResult Validate(string token)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret));

        var parameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = _options.Issuer,
            ValidAudience = _options.Audience,
            IssuerSigningKey = key,

            // no clock skew in demos so expiry is obvious
            ClockSkew = TimeSpan.Zero
        };

        try
    {
        var principal = _handler.ValidateToken(token, parameters, out _);
        return JwtValidationResult.Success(principal);
    }
    catch (SecurityTokenException ex)
    {
        return JwtValidationResult.Fail(ex.Message);
    }
    catch (Exception ex)
    {
        return JwtValidationResult.Fail($"Unexpected error: {ex.Message}");
    }
    }

    private static IEnumerable<Claim> BuildClaims(JwtSubject subject)
    {
        yield return new Claim(ClaimTypes.NameIdentifier, subject.UserId);
        yield return new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());

        foreach (var role in subject.Roles)
            yield return new Claim(ClaimTypes.Role, role);
    }
}