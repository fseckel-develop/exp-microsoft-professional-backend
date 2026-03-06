using Microsoft.Extensions.Options;
using SafeVault.Web.Configuration;
using SafeVault.Web.Models;
using SafeVault.Web.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SafeVault.Web.Tests.Services;

[TestFixture]
public sealed class JwtTokenServiceTests
{
    [Test]
    public void GenerateToken_ShouldIncludeUsernameAndRoleClaims()
    {
        var options = Options.Create(new JwtOptions
        {
            Issuer = "SafeVault",
            Audience = "SafeVaultClients",
            SecretKey = "this-is-a-very-long-test-secret-key-12345",
            ExpiryMinutes = 60
        });

        var service = new JwtTokenService(options);

        var user = new User
        {
            Id = 1,
            Username = "adminuser",
            Email = "admin@example.com",
            PasswordHash = "hash",
            Role = "Admin"
        };

        var token = service.GenerateToken(user);

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);

        Assert.That(jwt.Issuer, Is.EqualTo("SafeVault"));
        Assert.That(jwt.Audiences, Contains.Item("SafeVaultClients"));
        Assert.That(jwt.Claims.First(c => c.Type == ClaimTypes.Name).Value, Is.EqualTo("adminuser"));
        Assert.That(jwt.Claims.First(c => c.Type == ClaimTypes.Role).Value, Is.EqualTo("Admin"));
    }
}