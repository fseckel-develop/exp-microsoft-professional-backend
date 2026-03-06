namespace ExpirationPoliciesDemo.Models;

public sealed record SessionActivity(
    string SessionId,
    string Description,
    DateTime RecordedAtUtc
);