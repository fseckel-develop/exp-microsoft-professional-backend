using KnapsackMemoizationDemo.Benchmarking;
using KnapsackMemoizationDemo.Models;

namespace KnapsackMemoizationDemo.Presentation;

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

    public void WriteItems(IEnumerable<SupplyItem> items)
    {
        Console.WriteLine($"{"Item",-20} {"Weight",8} {"Value",8}");
        Console.WriteLine(new string('-', 40));

        foreach (var item in items)
        {
            Console.WriteLine($"{item.Name,-20} {item.Weight,8} {item.Value,8}");
        }

        Console.WriteLine();
    }

    public void WriteCapacity(int capacity)
    {
        Console.WriteLine($"Capacity: {capacity}");
        Console.WriteLine();
    }

    public void WriteReport(SolverReport report)
    {
        Console.WriteLine(report.SolverName);
        Console.WriteLine(new string('-', report.SolverName.Length));
        Console.WriteLine($"{"Best value",-12}: {report.BestValue,12:N0}");
        Console.WriteLine($"{"Time",-12}: {report.ElapsedMilliseconds,12:F4} ms");
        Console.WriteLine($"{"Call count",-12}: {report.CallCount,12:N0}");
        Console.WriteLine();
    }

    public void WriteComparisonNote()
    {
        Console.WriteLine("Note:");
        Console.WriteLine("The brute-force recursive solver explores many repeated subproblems.");
        Console.WriteLine("The memoized solver caches intermediate results and avoids redundant work.");
        Console.WriteLine();
    }
}