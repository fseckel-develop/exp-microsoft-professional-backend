using System.Diagnostics;
using SortingShowcaseDemo.Algorithms;

namespace SortingShowcaseDemo.Benchmarking;

public sealed class SortBenchmark<T>
{
    public SortReport Run(
        ISorter<T> sorter,
        T[] source,
        SortScenario<T> scenario)
    {
        var copy = (T[])source.Clone();

        var stopwatch = Stopwatch.StartNew();
        sorter.Sort(copy, scenario.Comparison);
        stopwatch.Stop();

        bool sortedCorrectly = IsSorted(copy, scenario.Comparison);

        return new SortReport(
            Algorithm: sorter.Name,
            Scenario: scenario.Name,
            ElapsedMilliseconds: stopwatch.ElapsedMilliseconds,
            IsSortedCorrectly: sortedCorrectly,
            FirstItemSummary: copy.Length > 0 ? scenario.Summarize(copy[0]) : "(none)",
            LastItemSummary: copy.Length > 0 ? scenario.Summarize(copy[^1]) : "(none)");
    }

    private static bool IsSorted(T[] items, Comparison<T> comparison)
    {
        for (int i = 1; i < items.Length; i++)
        {
            if (comparison(items[i - 1], items[i]) > 0)
                return false;
        }

        return true;
    }
}