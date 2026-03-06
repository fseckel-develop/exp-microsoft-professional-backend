namespace ExpirationPoliciesDemo.Models;

public sealed record ResetToken(
    string UserId,
    string Token,
    DateTime CreatedAtUtc
);