using SafeVault.Web.Models;

namespace SafeVault.Web.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}