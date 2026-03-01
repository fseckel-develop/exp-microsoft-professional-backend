using ClientGenerationDemo.Presentation;
using ClientGenerationDemo.Services;

namespace ClientGenerationDemo.Demo;

public sealed class ManualClientDemo
{
    private readonly ManualApiClient _client;
    private readonly ConsoleWriter _writer;

    public ManualClientDemo(ManualApiClient client, ConsoleWriter writer)
    {
        _client = client;
        _writer = writer;
    }

    public async Task RunAsync()
    {
        _writer.WriteSection("Manual Client Demo");

        var recipes = await _client.GetRecipesAsync();
        _writer.WriteRecipes(recipes);
    }
}