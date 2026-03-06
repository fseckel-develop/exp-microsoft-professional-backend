using Microsoft.Extensions.Caching.Memory;
using ExpirationPoliciesDemo.Models;
using ExpirationPoliciesDemo.Infrastructure;
using ExpirationPoliciesDemo.Presentation;

namespace ExpirationPoliciesDemo.Scenarios;

public sealed class SlidingExpirationScenario : IScenario
{
    private readonly ConsoleWriter _writer;

    public SlidingExpirationScenario(ConsoleWriter writer)
    {
        _writer = writer;
    }

    public async Task RunAsync()
    {
        _writer.Section("Sliding Expiration: Active User Session");

        using IMemoryCache cache = MemoryCacheFactory.Create();

        const string cacheKey = "auth:session:user-42";

        var session = new Session(
            UserId: "user-42",
            SessionId: "session-001",
            StartedAtUtc: DateTime.UtcNow);

        cache.Set(cacheKey, session, new MemoryCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromSeconds(5)
        });

        _writer.Info("Stored active session with 5-second sliding expiration.");

        for (int i = 1; i <= 3; i++)
        {
            await Task.Delay(TimeSpan.FromSeconds(3));

            if (cache.TryGetValue(cacheKey, out Session? cachedSession))
            {
                _writer.Info($"Access #{i}: hit -> {cachedSession!.SessionId}");
            }
            else
            {
                _writer.Info($"Access #{i}: miss -> session expired");
                return;
            }
        }

        _writer.Info("No more activity. Waiting long enough for expiration...");
        await Task.Delay(TimeSpan.FromSeconds(6));

        if (cache.TryGetValue(cacheKey, out _))
        {
            _writer.Info("Unexpected result: session still active.");
        }
        else
        {
            _writer.Info("Expected result: session expired after inactivity.");
        }
    }
}