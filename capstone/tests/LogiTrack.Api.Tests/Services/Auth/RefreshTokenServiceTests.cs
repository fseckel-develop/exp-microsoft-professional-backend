using FluentAssertions;
using LogiTrack.Api.Services.Auth;

namespace LogiTrack.Api.Tests.Services.Auth;

public class RefreshTokenServiceTests
{
    [Fact]
    public void GenerateRefreshToken_ShouldReturnNonEmptyBase64String()
    {
        var service = new RefreshTokenService();

        var token = service.GenerateRefreshToken();

        token.Should().NotBeNullOrWhiteSpace();

        var act = () => Convert.FromBase64String(token);
        act.Should().NotThrow();
    }

    [Fact]
    public void GenerateRefreshToken_ShouldGenerateDifferentValues()
    {
        var service = new RefreshTokenService();

        var token1 = service.GenerateRefreshToken();
        var token2 = service.GenerateRefreshToken();

        token1.Should().NotBe(token2);
    }
}