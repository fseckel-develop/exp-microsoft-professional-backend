using System.Text.Json;
using ClientGenerationDemo.Models;

namespace ClientGenerationDemo.Services;

public sealed class ManualApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public ManualApiClient(HttpClient httpClient, string baseUrl)
    {
        _httpClient = httpClient;
        _baseUrl = baseUrl.TrimEnd('/');
    }

    public async Task<IReadOnlyList<Recipe>> GetRecipesAsync()
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/recipes");

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException(
                $"Failed to fetch recipes. Status: {(int)response.StatusCode}");
        }

        await using var stream = await response.Content.ReadAsStreamAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var recipes = await JsonSerializer.DeserializeAsync<List<Recipe>>(stream, options);
        return recipes ?? [];
    }
}