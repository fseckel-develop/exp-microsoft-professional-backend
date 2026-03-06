using StackExchange.Redis;

namespace ExpirationPoliciesDemo.Infrastructure;

public static class RedisConnectionFactory
{
    public static ConnectionMultiplexer Create()
        => ConnectionMultiplexer.Connect("localhost");
}