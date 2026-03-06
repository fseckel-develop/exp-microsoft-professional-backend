using SafeVault.Web.Contracts;
using SafeVault.Web.Data;
using SafeVault.Web.Models;

namespace SafeVault.Web.Services;

public sealed class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }

    public Task<bool> UserExistsAsync(string username, CancellationToken ct = default)
    {
        return _userRepository.UserExistsAsync(username, ct);
    }

    public async Task RegisterAsync(RegisterRequestDto dto, CancellationToken ct = default)
    {
        var user = new User
        {
            Username = dto.Username.Trim(),
            Email = dto.Email.Trim(),
            PasswordHash = _passwordHasher.HashPassword(dto.Password),
            Role = dto.Role.Trim()
        };

        await _userRepository.InsertUserAsync(user, ct);
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto dto, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByUsernameAsync(dto.Username.Trim(), ct);

        if (user is null)
            return null;

        var validPassword = _passwordHasher.VerifyPassword(dto.Password, user.PasswordHash);
        if (!validPassword)
            return null;

        var token = _jwtTokenService.GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token,
            Username = user.Username,
            Role = user.Role
        };
    }
}