using System.Diagnostics;
using SearchingAlgorithmsDemo.Algorithms;

namespace SearchingAlgorithmsDemo.Benchmarking;

public sealed class SearchBenchmark<T, TKey>
{
    public SearchReport Run(
        ISearchStrategy<T, TKey> strategy,
        SearchScenario<T, TKey> scenario)
    {
        var stopwatch = Stopwatch.StartNew();

        int index = strategy.FindIndex(
            scenario.Dataset,
            scenario.TargetKey,
            scenario.KeySelector,
            scenario.Comparer);

        stopwatch.Stop();

        bool found = index >= 0;
        string? summary = found ? scenario.Summarize(scenario.Dataset[index]) : null;

        return new SearchReport(
            Algorithm: strategy.Name,
            Scenario: scenario.Name,
            ElapsedMilliseconds: stopwatch.ElapsedMilliseconds,
            FoundIndex: index,
            WasFound: found,
            ItemSummary: summary);
    }
}