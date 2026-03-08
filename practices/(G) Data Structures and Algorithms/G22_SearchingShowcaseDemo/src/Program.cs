using SearchingAlgorithmsDemo.Algorithms;
using SearchingAlgorithmsDemo.Benchmarking;
using SearchingAlgorithmsDemo.Data;
using SearchingAlgorithmsDemo.Presentation;

namespace SearchingAlgorithmsDemo;

internal static class Program
{
    private static void Main()
    {
        const int employeeCount = 1_000_000;
        const int existingTarget = 599_876;
        const int missingTarget = 999_999;

        var sortedDirectory = EmployeeDirectoryFactory.CreateDirectory(employeeCount);
        var unsortedDirectory = EmployeeDirectoryFactory.Shuffle(sortedDirectory);

        var linearSearch = new LinearSearch<EmployeeRecord, int>();
        var binarySearch = new BinarySearch<EmployeeRecord, int>();
        var benchmark = new SearchBenchmark<EmployeeRecord, int>();
        var writer = new ConsoleReportWriter();

        Func<EmployeeRecord, int> keySelector = employee => employee.EmployeeNumber;
        IComparer<int> comparer = Comparer<int>.Default;
        Func<EmployeeRecord, string> summarize =
            employee => $"{employee.EmployeeNumber} - {employee.FullName} ({employee.Department})";

        var linearFoundScenario = new SearchScenario<EmployeeRecord, int>
        {
            Name = "Unsorted employee directory lookup",
            Description = "Linear search over a shuffled employee directory.",
            Dataset = unsortedDirectory,
            TargetKey = existingTarget,
            KeySelector = keySelector,
            Comparer = comparer,
            Summarize = summarize
        };

        var binaryFoundScenario = new SearchScenario<EmployeeRecord, int>
        {
            Name = "Sorted employee directory lookup",
            Description = "Binary search over an employee directory sorted by employee number.",
            Dataset = sortedDirectory,
            TargetKey = existingTarget,
            KeySelector = keySelector,
            Comparer = comparer,
            Summarize = summarize
        };

        var binaryMissingScenario = new SearchScenario<EmployeeRecord, int>
        {
            Name = "Missing employee lookup",
            Description = "Binary search for an employee number that does not exist.",
            Dataset = sortedDirectory,
            TargetKey = missingTarget,
            KeySelector = keySelector,
            Comparer = comparer,
            Summarize = summarize
        };

        writer.WriteIntro("Searching Algorithms Showcase");

        writer.WriteScenarioHeader(
            linearFoundScenario.Name,
            linearFoundScenario.Description,
            linearFoundScenario.Dataset.Length,
            linearFoundScenario.TargetKey);

        writer.WriteReport(benchmark.Run(linearSearch, linearFoundScenario));

        writer.WriteScenarioHeader(
            binaryFoundScenario.Name,
            binaryFoundScenario.Description,
            binaryFoundScenario.Dataset.Length,
            binaryFoundScenario.TargetKey);

        writer.WriteReport(benchmark.Run(binarySearch, binaryFoundScenario));

        writer.WriteScenarioHeader(
            binaryMissingScenario.Name,
            binaryMissingScenario.Description,
            binaryMissingScenario.Dataset.Length,
            binaryMissingScenario.TargetKey);

        writer.WriteReport(benchmark.Run(binarySearch, binaryMissingScenario));

        writer.WriteObservation(
            "- Linear search works on any sequence but scales poorly.\n" +
            "- Binary search is much faster on large datasets, but it requires sorted input.");
    }
}