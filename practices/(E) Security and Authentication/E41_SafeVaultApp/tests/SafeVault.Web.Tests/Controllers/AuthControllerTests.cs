using Microsoft.AspNetCore.Mvc;
using Moq;
using SafeVault.Web.Contracts;
using SafeVault.Web.Controllers;
using SafeVault.Web.Services;

namespace SafeVault.Web.Tests.Controllers;

[TestFixture]
public sealed class AuthControllerTests
{
    private Mock<IAuthService> _authService = null!;
    private AuthController _controller = null!;

    [SetUp]
    public void SetUp()
    {
        _authService = new Mock<IAuthService>();
        _controller = new AuthController(_authService.Object);
    }

    [Test]
    public async Task Register_ShouldReturnConflict_WhenUsernameAlreadyExists()
    {
        var dto = new RegisterRequestDto
        {
            Username = "user",
            Email = "user@example.com",
            Password = "Password123!",
            Role = "User"
        };

        _authService
            .Setup(s => s.UserExistsAsync("user", It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await _controller.Register(dto, CancellationToken.None);

        Assert.That(result, Is.InstanceOf<ConflictObjectResult>());
    }

    [Test]
    public async Task Register_ShouldReturnOk_WhenRegistrationSucceeds()
    {
        var dto = new RegisterRequestDto
        {
            Username = "user",
            Email = "user@example.com",
            Password = "Password123!",
            Role = "User"
        };

        _authService
            .Setup(s => s.UserExistsAsync("user", It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _authService
            .Setup(s => s.RegisterAsync(dto, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var result = await _controller.Register(dto, CancellationToken.None);

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task Login_ShouldReturnUnauthorized_WhenCredentialsAreInvalid()
    {
        var dto = new LoginRequestDto
        {
            Username = "user",
            Password = "WrongPassword!"
        };

        _authService
            .Setup(s => s.LoginAsync(dto, It.IsAny<CancellationToken>()))
            .ReturnsAsync((AuthResponseDto?)null);

        var result = await _controller.Login(dto, CancellationToken.None);

        Assert.That(result.Result, Is.InstanceOf<UnauthorizedObjectResult>());
    }

    [Test]
    public async Task Login_ShouldReturnOk_WhenCredentialsAreValid()
    {
        var dto = new LoginRequestDto
        {
            Username = "user",
            Password = "Password123!"
        };

        var response = new AuthResponseDto
        {
            Token = "jwt-token",
            Username = "user",
            Role = "User"
        };

        _authService
            .Setup(s => s.LoginAsync(dto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var result = await _controller.Login(dto, CancellationToken.None);

        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }
}