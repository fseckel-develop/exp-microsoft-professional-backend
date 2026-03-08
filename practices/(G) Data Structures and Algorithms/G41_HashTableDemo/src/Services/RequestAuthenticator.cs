using HashTableDemo.Models;

namespace HashTableDemo.Services;

public sealed class RequestAuthenticator
{
    private readonly ApiKeyRegistry _registry;

    public RequestAuthenticator(ApiKeyRegistry registry)
    {
        _registry = registry;
    }

    public AuthenticationResult Authenticate(ApiRequest request)
    {
        if (_registry.TryGetClient(request.ApiKey, out var client) && client is not null)
        {
            return new AuthenticationResult(
                IsAuthenticated: true,
                ApiKey: request.ApiKey,
                Path: request.Path,
                Client: client,
                Message: "Request authenticated.");
        }

        return new AuthenticationResult(
            IsAuthenticated: false,
            ApiKey: request.ApiKey,
            Path: request.Path,
            Client: null,
            Message: "API key is invalid or revoked.");
    }
}