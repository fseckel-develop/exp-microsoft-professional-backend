using System.Security.Claims;

namespace JwtAuthenticationDemo.Services;

public interface IJwtTokenService
{
    string CreateToken(string subject, IEnumerable<Claim>? extraClaims = null);
}