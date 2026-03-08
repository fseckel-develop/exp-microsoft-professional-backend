using SortingShowcaseDemo.Benchmarking;

namespace SortingShowcaseDemo.Presentation;

public sealed class ConsoleReportWriter
{
    public void WriteIntro(string title)
    {
        Console.WriteLine(title);
        Console.WriteLine(new string('-', title.Length));
        Console.WriteLine();
    }

    public void WriteScenarioHeader(string scenarioName, string scenarioDescription, int count)
    {
        Console.WriteLine($"Scenario: {scenarioName}");
        Console.WriteLine($"Description: {scenarioDescription}");
        Console.WriteLine($"Dataset size: {count} items");
        Console.WriteLine();
    }

    public void WriteReport(SortReport report)
    {
        Console.WriteLine($"{report.Algorithm}:");
        Console.WriteLine($"  Scenario: {report.Scenario}");
        Console.WriteLine($"  Time: {report.ElapsedMilliseconds} ms");
        Console.WriteLine($"  Sorted correctly: {report.IsSortedCorrectly}");
        Console.WriteLine($"  First item: {report.FirstItemSummary}");
        Console.WriteLine($"  Last item: {report.LastItemSummary}");
        Console.WriteLine();
    }

    public void WriteObservation(string observation)
    {
        Console.WriteLine("Observation:");
        Console.WriteLine(observation);
    }
}