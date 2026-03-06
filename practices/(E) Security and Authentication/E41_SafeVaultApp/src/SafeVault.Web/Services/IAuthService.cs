using SafeVault.Web.Contracts;

namespace SafeVault.Web.Services;

public interface IAuthService
{
    Task<bool> UserExistsAsync(string username, CancellationToken ct = default);
    Task RegisterAsync(RegisterRequestDto dto, CancellationToken ct = default);
    Task<AuthResponseDto?> LoginAsync(LoginRequestDto dto, CancellationToken ct = default);
}