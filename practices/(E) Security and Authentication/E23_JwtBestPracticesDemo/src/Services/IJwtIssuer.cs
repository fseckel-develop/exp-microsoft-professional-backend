using System.Security.Claims;

namespace JwtBestPracticesDemo.Services;

public interface IJwtIssuer
{
    string CreateAccessToken(string username, string role);
}