using SearchingAlgorithmsDemo.Benchmarking;

namespace SearchingAlgorithmsDemo.Presentation;

public sealed class ConsoleReportWriter
{
    public void WriteIntro(string title)
    {
        Console.WriteLine(title);
        Console.WriteLine(new string('-', title.Length));
        Console.WriteLine();
    }

    public void WriteScenarioHeader(string name, string description, int datasetSize, object target)
    {
        Console.WriteLine($"Scenario: {name}");
        Console.WriteLine($"Description: {description}");
        Console.WriteLine($"Dataset size: {datasetSize}");
        Console.WriteLine($"Target key: {target}");
        Console.WriteLine();
    }

    public void WriteReport(SearchReport report)
    {
        Console.WriteLine($"{report.Algorithm}:");
        Console.WriteLine($"  Time: {report.ElapsedMilliseconds} ms");
        Console.WriteLine($"  Found: {report.WasFound}");
        Console.WriteLine($"  Index: {report.FoundIndex}");
        Console.WriteLine($"  Item: {report.ItemSummary ?? "(not found)"}");
        Console.WriteLine();
    }

    public void WriteObservation(string text)
    {
        Console.WriteLine("Observation:");
        Console.WriteLine(text);
    }
}