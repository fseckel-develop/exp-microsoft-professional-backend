using StackExchange.Redis;
using ExpirationPoliciesDemo.Models;
using ExpirationPoliciesDemo.Infrastructure;
using ExpirationPoliciesDemo.Presentation;

namespace ExpirationPoliciesDemo.Scenarios;

public sealed class DependentExpirationScenario : IScenario
{
    private readonly ConsoleWriter _writer;

    public DependentExpirationScenario(ConsoleWriter writer)
    {
        _writer = writer;
    }

    public async Task RunAsync()
    {
        _writer.Section("Related Cleanup: Session Expiration Removes Activity History");

        using ConnectionMultiplexer redis = RedisConnectionFactory.Create();
        IDatabase db = redis.GetDatabase();

        const string sessionKey = "auth:redis:session:user-42";
        const string activityKey = "auth:redis:activity:user-42";

        var session = new Session("user-42", "redis-session-001", DateTime.UtcNow);
        var activity = new SessionActivity(session.SessionId, "User logged in", DateTime.UtcNow);

        await db.StringSetAsync(sessionKey, session.SessionId);
        await db.StringSetAsync(activityKey, $"{activity.Description} at {activity.RecordedAtUtc:O}");

        _writer.Info("Stored session and related activity history in Redis.");

        var ttl = TimeSpan.FromSeconds(10);
        await db.KeyExpireAsync(sessionKey, ttl);

        _writer.Info($"Applied TTL of {ttl.TotalSeconds} seconds to session key.");
        _writer.Info("Waiting for session expiration...");

        await Task.Delay(TimeSpan.FromSeconds(11));

        bool sessionExists = await db.KeyExistsAsync(sessionKey);

        if (!sessionExists)
        {
            await db.KeyDeleteAsync(activityKey);
            _writer.Info("Session expired. Related activity history was deleted.");
        }
        else
        {
            _writer.Info("Session still active.");
        }
    }
}