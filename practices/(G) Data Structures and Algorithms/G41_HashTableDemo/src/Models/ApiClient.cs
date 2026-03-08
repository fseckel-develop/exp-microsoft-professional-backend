namespace HashTableDemo.Models;

public sealed record ApiClient(
    string ClientId,
    string DisplayName,
    string Tier
);