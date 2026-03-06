using Moq;
using SafeVault.Web.Contracts;
using SafeVault.Web.Data;
using SafeVault.Web.Models;
using SafeVault.Web.Services;

namespace SafeVault.Web.Tests.Services;

[TestFixture]
public sealed class AuthServiceTests
{
    private Mock<IUserRepository> _userRepository = null!;
    private Mock<IPasswordHasher> _passwordHasher = null!;
    private Mock<IJwtTokenService> _jwtTokenService = null!;
    private AuthService _authService = null!;

    [SetUp]
    public void SetUp()
    {
        _userRepository = new Mock<IUserRepository>();
        _passwordHasher = new Mock<IPasswordHasher>();
        _jwtTokenService = new Mock<IJwtTokenService>();

        _authService = new AuthService(
            _userRepository.Object,
            _passwordHasher.Object,
            _jwtTokenService.Object);
    }

    [Test]
    public async Task UserExistsAsync_ShouldDelegateToRepository()
    {
        _userRepository
            .Setup(r => r.UserExistsAsync("user", It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await _authService.UserExistsAsync("user");

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task RegisterAsync_ShouldHashPassword_AndInsertUser()
    {
        var dto = new RegisterRequestDto
        {
            Username = "user",
            Email = "user@example.com",
            Password = "Password123!",
            Role = "User"
        };

        _passwordHasher
            .Setup(h => h.HashPassword(dto.Password))
            .Returns("hashed-password");

        User? insertedUser = null;

        _userRepository
            .Setup(r => r.InsertUserAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Callback<User, CancellationToken>((u, _) => insertedUser = u)
            .ReturnsAsync((User u, CancellationToken _) =>
            {
                u.Id = 1;
                return u;
            });

        await _authService.RegisterAsync(dto);

        Assert.That(insertedUser, Is.Not.Null);
        Assert.That(insertedUser!.Username, Is.EqualTo("user"));
        Assert.That(insertedUser.Email, Is.EqualTo("user@example.com"));
        Assert.That(insertedUser.PasswordHash, Is.EqualTo("hashed-password"));
        Assert.That(insertedUser.Role, Is.EqualTo("User"));
    }

    [Test]
    public async Task LoginAsync_ShouldReturnNull_WhenUserDoesNotExist()
    {
        _userRepository
            .Setup(r => r.GetByUsernameAsync("ghost", It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        var result = await _authService.LoginAsync(new LoginRequestDto
        {
            Username = "ghost",
            Password = "anything"
        });

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task LoginAsync_ShouldReturnNull_WhenPasswordIsInvalid()
    {
        var user = new User
        {
            Id = 1,
            Username = "user",
            Email = "user@example.com",
            PasswordHash = "hashed-password",
            Role = "User"
        };

        _userRepository
            .Setup(r => r.GetByUsernameAsync("user", It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _passwordHasher
            .Setup(h => h.VerifyPassword("WrongPassword!", "hashed-password"))
            .Returns(false);

        var result = await _authService.LoginAsync(new LoginRequestDto
        {
            Username = "user",
            Password = "WrongPassword!"
        });

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task LoginAsync_ShouldReturnAuthResponse_WhenCredentialsAreValid()
    {
        var user = new User
        {
            Id = 1,
            Username = "adminuser",
            Email = "admin@example.com",
            PasswordHash = "hashed-password",
            Role = "Admin"
        };

        _userRepository
            .Setup(r => r.GetByUsernameAsync("adminuser", It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _passwordHasher
            .Setup(h => h.VerifyPassword("Password123!", "hashed-password"))
            .Returns(true);

        _jwtTokenService
            .Setup(t => t.GenerateToken(user))
            .Returns("jwt-token");

        var result = await _authService.LoginAsync(new LoginRequestDto
        {
            Username = "adminuser",
            Password = "Password123!"
        });

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Token, Is.EqualTo("jwt-token"));
        Assert.That(result.Username, Is.EqualTo("adminuser"));
        Assert.That(result.Role, Is.EqualTo("Admin"));
    }
}