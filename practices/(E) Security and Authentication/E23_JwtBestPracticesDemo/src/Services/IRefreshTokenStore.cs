namespace JwtBestPracticesDemo.Services;

public interface IRefreshTokenStore
{
    // returns refresh token string
    string Issue(string username, string role, DateTimeOffset expiresAt);

    // validates and rotates: consumes old, returns data for issuing new access token + new refresh token
    bool TryConsume(string refreshToken, out (string Username, string Role) identity);
}