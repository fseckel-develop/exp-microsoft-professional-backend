using ClientGenerationDemo.Models;

namespace ClientGenerationDemo.Presentation;

public sealed class ConsoleWriter
{
    public void WriteTitle(string title)
    {
        Console.WriteLine(title);
        Console.WriteLine(new string('=', title.Length));
        Console.WriteLine();
    }

    public void WriteSection(string title)
    {
        Console.WriteLine(title);
        Console.WriteLine(new string('-', title.Length));
    }

    public void WriteRecipes(IEnumerable<Recipe> recipes)
    {
        foreach (var recipe in recipes)
        {
            Console.WriteLine($"{recipe.Name}: {recipe.Instructions}");
        }

        Console.WriteLine();
    }

    public void WriteGeneratedRecipes(IEnumerable<dynamic> recipes)
    {
        foreach (var recipe in recipes)
        {
            Console.WriteLine($"{recipe.Name}: {recipe.Instructions}");
        }

        Console.WriteLine();
    }

    public void WriteMessage(string message)
    {
        Console.WriteLine(message);
        Console.WriteLine();
    }
}