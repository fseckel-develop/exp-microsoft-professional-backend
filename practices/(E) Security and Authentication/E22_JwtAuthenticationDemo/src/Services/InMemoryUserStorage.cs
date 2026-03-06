namespace JwtAuthenticationDemo.Services;

public sealed class InMemoryUserStore : IUserStorage
{
    // Demo-only
    private readonly Dictionary<string, string> _users = new(StringComparer.OrdinalIgnoreCase)
    {
        ["testuser"] = "password123"
    };

    public bool ValidateCredentials(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            return false;

        return _users.TryGetValue(username.Trim(), out var storedPw) && storedPw == password;
    }
}