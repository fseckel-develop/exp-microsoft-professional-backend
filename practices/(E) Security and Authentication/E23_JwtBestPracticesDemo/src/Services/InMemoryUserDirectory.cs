namespace JwtBestPracticesDemo.Services;

public sealed class InMemoryUserDirectory : IUserDirectory
{
    // demo-only: username -> (password, role)
    private readonly Dictionary<string, (string Pw, string Role)> _users =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["admin"] = ("admin123", "Admin"),
            ["user"]  = ("user123",  "User"),
            ["user1"] = ("password1","User")
        };

    public bool TryValidate(string username, string password, out string role)
    {
        role = string.Empty;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            return false;

        if (!_users.TryGetValue(username.Trim(), out var data))
            return false;

        if (data.Pw != password)
            return false;

        role = data.Role;
        return true;
    }
}