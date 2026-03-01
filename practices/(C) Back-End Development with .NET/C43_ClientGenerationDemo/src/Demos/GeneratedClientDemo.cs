#if GENERATED_CLIENT
using ClientGenerationDemo.Presentation;
using GeneratedRecipeApi;

namespace ClientGenerationDemo.Demo;

public sealed class GeneratedClientDemo
{
    private readonly GeneratedApiClient _client;
    private readonly ConsoleWriter _writer;

    public GeneratedClientDemo(GeneratedApiClient client, ConsoleWriter writer)
    {
        _client = client;
        _writer = writer;
    }

    public async Task RunAsync()
    {
        _writer.WriteSection("Generated Client Demo");

        var newRecipe = new CreateRecipeRequestDto
        {
            Name = "Roasted Veggie Wrap",
            Instructions = "Roast veggies, warm tortilla, add sauce, wrap and serve."
        };

        await _client.CreateRecipeAsync(newRecipe);

        var recipes = await _client.GetAllRecipesAsync();

        foreach (var recipe in recipes)
        {
            _writer.WriteMessage($"{recipe.Name}: {recipe.Instructions}");
        }
    }
}
#endif