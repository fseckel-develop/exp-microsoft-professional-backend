using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;

namespace JwtBestPracticesDemo.Services;

public sealed class InMemoryRefreshTokenStore : IRefreshTokenStore
{
    private sealed record Entry(string Username, string Role, DateTimeOffset ExpiresAt);

    private readonly ConcurrentDictionary<string, Entry> _tokens = new();

    public string Issue(string username, string role, DateTimeOffset expiresAt)
    {
        var raw = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");
        var token = ToUrlSafeToken(raw);

        _tokens[token] = new Entry(username, role, expiresAt);
        return token;
    }

    public bool TryConsume(string refreshToken, out (string Username, string Role) identity)
    {
        identity = default;

        if (string.IsNullOrWhiteSpace(refreshToken))
            return false;

        if (!_tokens.TryRemove(refreshToken, out var entry))
            return false;

        if (entry.ExpiresAt <= DateTimeOffset.UtcNow)
            return false;

        identity = (entry.Username, entry.Role);
        return true;
    }

    private static string ToUrlSafeToken(string input)
    {
        // Not required for security, just makes a nicer-looking token for demos
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(bytes)
            .Replace("+", "-")
            .Replace("/", "_")
            .TrimEnd('=');
    }
}