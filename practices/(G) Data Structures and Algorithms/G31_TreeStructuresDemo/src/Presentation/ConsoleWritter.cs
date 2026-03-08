using TreeStructuresDemo.Models;

namespace TreeStructuresDemo.Presentation;

public sealed class ConsoleWriter
{
    public void WriteTitle(string title)
    {
        Console.WriteLine(title);
        Console.WriteLine(new string('=', title.Length));
        Console.WriteLine();
    }

    public void WriteSection(string title, string description)
    {
        Console.WriteLine(title);
        Console.WriteLine(new string('-', title.Length));
        Console.WriteLine(description);
        Console.WriteLine();
    }

    public void WriteTraversal(string label, string content)
    {
        Console.WriteLine($"{label,-20} {content}");
    }

    public void WriteMetric(string label, object? value)
    {
        Console.WriteLine($"{label,-20} {value}");
    }

    public void WriteSearchComparison(
        int searchId,
        SearchResult<ContentItem> bstResult,
        SearchResult<ContentItem> avlResult)
    {
        Console.WriteLine($"Search comparison for content id {searchId}:");
        Console.WriteLine($"{ "BST found",-20} {bstResult.Found}");
        Console.WriteLine($"{ "BST steps",-20} {bstResult.Steps}");
        Console.WriteLine($"{ "AVL found",-20} {avlResult.Found}");
        Console.WriteLine($"{ "AVL steps",-20} {avlResult.Steps}");

        if (bstResult.Value is not null)
        {
            Console.WriteLine($"{ "Found item",-20} {bstResult.Value.Id}:{bstResult.Value.Title}");
        }
    }

    public void WriteSpacer()
    {
        Console.WriteLine();
        Console.WriteLine(new string('-', 60));
        Console.WriteLine();
    }
}