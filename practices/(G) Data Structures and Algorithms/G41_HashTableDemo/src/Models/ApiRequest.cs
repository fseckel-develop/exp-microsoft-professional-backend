namespace HashTableDemo.Models;

public sealed record ApiRequest(
    string ApiKey,
    string Path
);