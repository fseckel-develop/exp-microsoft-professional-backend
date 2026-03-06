namespace ExpirationPoliciesDemo.Models;

public sealed record Session(
    string UserId,
    string SessionId,
    DateTime StartedAtUtc
);