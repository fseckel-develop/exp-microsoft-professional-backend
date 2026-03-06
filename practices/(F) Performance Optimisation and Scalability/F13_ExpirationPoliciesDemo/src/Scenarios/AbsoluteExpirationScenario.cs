using Microsoft.Extensions.Caching.Memory;
using ExpirationPoliciesDemo.Models;
using ExpirationPoliciesDemo.Infrastructure;
using ExpirationPoliciesDemo.Presentation;

namespace ExpirationPoliciesDemo.Scenarios;

public sealed class AbsoluteExpirationScenario : IScenario
{
    private readonly ConsoleWriter _writer;

    public AbsoluteExpirationScenario(ConsoleWriter writer)
    {
        _writer = writer;
    }

    public async Task RunAsync()
    {
        _writer.Section("Absolute Expiration: Password Reset Token");

        using IMemoryCache cache = MemoryCacheFactory.Create();

        const string cacheKey = "auth:reset-token:user-42";

        var token = new ResetToken(
            UserId: "user-42",
            Token: "reset-token-abc123",
            CreatedAtUtc: DateTime.UtcNow);

        cache.Set(cacheKey, token, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
        });

        _writer.Info("Stored password reset token with 10-second absolute expiration.");

        await ReadTokenAsync(cache, cacheKey, "Initial read");
        await Task.Delay(TimeSpan.FromSeconds(5));
        await ReadTokenAsync(cache, cacheKey, "Read after 5 seconds");
        await Task.Delay(TimeSpan.FromSeconds(6));
        await ReadTokenAsync(cache, cacheKey, "Read after 11 seconds");
    }

    private Task ReadTokenAsync(IMemoryCache cache, string key, string label)
    {
        if (cache.TryGetValue(key, out ResetToken? token))
        {
            _writer.Info($"{label}: hit -> {token!.Token}");
        }
        else
        {
            _writer.Info($"{label}: miss -> token expired");
        }

        return Task.CompletedTask;
    }
}