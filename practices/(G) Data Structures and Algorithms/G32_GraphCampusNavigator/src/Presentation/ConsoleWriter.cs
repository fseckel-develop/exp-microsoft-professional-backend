using GraphCampusNavigator.Models;
using GraphCampusNavigator.Graphs;

namespace GraphCampusNavigator.Presentation;

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

    public void WriteAdjacencyList(UnweightedGraph graph)
    {
        foreach (var node in graph.Adjacency.OrderBy(x => x.Key))
        {
            Console.WriteLine($"{node.Key,-15} -> {string.Join(", ", node.Value)}");
        }

        Console.WriteLine();
    }

    public void WriteWeightedAdjacencyList(WeightedGraph graph)
    {
        foreach (var node in graph.Adjacency.OrderBy(x => x.Key))
        {
            var formatted = node.Value.Select(edge => $"{edge.To}({edge.Weight})");
            Console.WriteLine($"{node.Key,-15} -> {string.Join(", ", formatted)}");
        }

        Console.WriteLine();
    }

    public void WriteTraversal(string label, IEnumerable<string> nodes)
    {
        Console.WriteLine($"{label,-20} {string.Join(" -> ", nodes)}");
    }

    public void WriteDistances(string start, Dictionary<string, int> distances)
    {
        Console.WriteLine($"Shortest distances from {start}:");
        foreach (var entry in distances.OrderBy(x => x.Value))
        {
            string distance = entry.Value == int.MaxValue ? "Unreachable" : entry.Value.ToString();
            Console.WriteLine($"{entry.Key,-15} {distance}");
        }

        Console.WriteLine();
    }

    public void WritePathResult(string label, PathResult result)
    {
        Console.WriteLine(label);

        if (!result.Found)
        {
            Console.WriteLine("  No path found.");
            Console.WriteLine();
            return;
        }

        Console.WriteLine($"  Path: {string.Join(" -> ", result.Path)}");
        Console.WriteLine($"  Total cost: {result.TotalCost}");
        Console.WriteLine();
    }

    public void WriteSpacer()
    {
        Console.WriteLine(new string('-', 60));
        Console.WriteLine();
    }
}