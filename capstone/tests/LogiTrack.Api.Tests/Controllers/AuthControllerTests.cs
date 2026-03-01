using FluentAssertions;
using LogiTrack.Api.Contracts.Auth;
using LogiTrack.Api.Controllers;
using LogiTrack.Api.Models;
using LogiTrack.Api.Services.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Moq;

namespace LogiTrack.Api.Tests.Controllers;

public class AuthControllerTests
{
    private static Mock<UserManager<ApplicationUser>> CreateUserManagerMock()
    {
        var store = new Mock<IUserStore<ApplicationUser>>();
        return new Mock<UserManager<ApplicationUser>>(
            store.Object, null!, null!, null!, null!, null!, null!, null!, null!);
    }

    private static Mock<SignInManager<ApplicationUser>> CreateSignInManagerMock(UserManager<ApplicationUser> userManager)
    {
        var contextAccessor = new Mock<IHttpContextAccessor>();
        var claimsFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();

        return new Mock<SignInManager<ApplicationUser>>(
            userManager,
            contextAccessor.Object,
            claimsFactory.Object,
            null!, null!, null!, null!);
    }

    [Fact]
    public async Task Login_WhenUserMissing_ShouldReturnUnauthorized()
    {
        var userManagerMock = CreateUserManagerMock();
        var signInManagerMock = CreateSignInManagerMock(userManagerMock.Object);
        var emailSenderMock = new Mock<IEmailSender>();
        var jwtServiceMock = new Mock<IJwtTokenService>();
        var refreshServiceMock = new Mock<IRefreshTokenService>();
        var envMock = new Mock<IWebHostEnvironment>();

        userManagerMock.Setup(x => x.FindByEmailAsync("missing@example.com"))
            .ReturnsAsync((ApplicationUser?)null);

        var controller = new AuthController(
            userManagerMock.Object,
            signInManagerMock.Object,
            emailSenderMock.Object,
            jwtServiceMock.Object,
            refreshServiceMock.Object,
            envMock.Object);

        var result = await controller.Login(new LoginDto
        {
            Email = "missing@example.com",
            Password = "Password123!"
        });

        result.Result.Should().BeOfType<UnauthorizedObjectResult>();
    }

    [Fact]
    public async Task Login_WhenEmailNotConfirmed_ShouldReturnUnauthorized()
    {
        var user = new ApplicationUser
        {
            Id = "1",
            Email = "user@example.com",
            UserName = "user@example.com",
            EmailConfirmed = false
        };

        var userManagerMock = CreateUserManagerMock();
        var signInManagerMock = CreateSignInManagerMock(userManagerMock.Object);
        var emailSenderMock = new Mock<IEmailSender>();
        var jwtServiceMock = new Mock<IJwtTokenService>();
        var refreshServiceMock = new Mock<IRefreshTokenService>();
        var envMock = new Mock<IWebHostEnvironment>();

        userManagerMock.Setup(x => x.FindByEmailAsync(user.Email))
            .ReturnsAsync(user);

        var controller = new AuthController(
            userManagerMock.Object,
            signInManagerMock.Object,
            emailSenderMock.Object,
            jwtServiceMock.Object,
            refreshServiceMock.Object,
            envMock.Object);

        var result = await controller.Login(new LoginDto
        {
            Email = user.Email!,
            Password = "Password123!"
        });

        result.Result.Should().BeOfType<UnauthorizedObjectResult>();
    }
}