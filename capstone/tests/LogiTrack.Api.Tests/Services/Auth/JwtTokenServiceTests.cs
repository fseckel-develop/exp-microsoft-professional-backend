using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FluentAssertions;
using LogiTrack.Api.Models;
using LogiTrack.Api.Services.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;

namespace LogiTrack.Api.Tests.Services.Auth;

public class JwtTokenServiceTests
{
    private static Mock<UserManager<ApplicationUser>> CreateUserManagerMock()
    {
        var store = new Mock<IUserStore<ApplicationUser>>();
        return new Mock<UserManager<ApplicationUser>>(
            store.Object,
            null!, null!, null!, null!, null!, null!, null!, null!);
    }

    [Fact]
    public async Task GenerateTokenAsync_ShouldCreateValidJwt_WithExpectedClaims()
    {
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Jwt:Key"] = "supersecretkeysupersecretkey123!",
                ["Jwt:Issuer"] = "LogiTrackApi",
                ["Jwt:Audience"] = "LogiTrackClient"
            })
            .Build();

        var user = new ApplicationUser
        {
            Id = "user-1",
            Email = "admin@example.com",
            UserName = "admin@example.com"
        };

        var userManagerMock = CreateUserManagerMock();
        userManagerMock
            .Setup(x => x.GetRolesAsync(user))
            .ReturnsAsync(new List<string> { "Admin" });

        userManagerMock
            .Setup(x => x.GetSecurityStampAsync(user))
            .ReturnsAsync("security-stamp-123");

        var service = new JwtTokenService(config, userManagerMock.Object);

        var tokenString = await service.GenerateTokenAsync(user);

        tokenString.Should().NotBeNullOrWhiteSpace();

        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(tokenString);

        token.Issuer.Should().Be("LogiTrackApi");
        token.Audiences.Should().Contain("LogiTrackClient");
        token.Claims.Should().Contain(c => c.Type == ClaimTypes.NameIdentifier && c.Value == "user-1");
        token.Claims.Should().Contain(c => c.Type == ClaimTypes.Email && c.Value == "admin@example.com");
        token.Claims.Should().Contain(c => c.Type == ClaimTypes.Name && c.Value == "admin@example.com");
        token.Claims.Should().Contain(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
        token.Claims.Should().Contain(c => c.Type == "security_stamp" && c.Value == "security-stamp-123");
    }

    [Fact]
    public async Task GenerateTokenAsync_WhenJwtKeyMissing_ShouldThrow()
    {
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Jwt:Issuer"] = "LogiTrackApi",
                ["Jwt:Audience"] = "LogiTrackClient"
            })
            .Build();

        var user = new ApplicationUser
        {
            Id = "user-1",
            Email = "user@example.com",
            UserName = "user@example.com"
        };

        var userManagerMock = CreateUserManagerMock();
        userManagerMock.Setup(x => x.GetRolesAsync(user)).ReturnsAsync(new List<string>());
        userManagerMock.Setup(x => x.GetSecurityStampAsync(user)).ReturnsAsync("stamp");

        var service = new JwtTokenService(config, userManagerMock.Object);

        var act = async () => await service.GenerateTokenAsync(user);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("*JWT Key is not configured*");
    }
}