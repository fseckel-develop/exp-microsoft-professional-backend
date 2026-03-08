namespace HashTableDemo.Models;

public sealed record AuthenticationResult(
    bool IsAuthenticated,
    string ApiKey,
    string Path,
    ApiClient? Client,
    string Message
);