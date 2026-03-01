namespace ClientGenerationDemo.Configuration;

public sealed class ApiClientOptions
{
    public string BaseUrl { get; init; } = "http://localhost:5221";
    public string SwaggerPath { get; init; } = "/swagger/v1/swagger.json";
    public string GeneratedClientName { get; init; } = "GeneratedApiClient";
    public string GeneratedClientNamespace { get; init; } = "GeneratedRecipeApi";
}