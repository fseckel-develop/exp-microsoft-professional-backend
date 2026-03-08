namespace SortingShowcaseDemo.Benchmarking;

public sealed record SortReport(
    string Algorithm,
    string Scenario,
    long ElapsedMilliseconds,
    bool IsSortedCorrectly,
    string FirstItemSummary,
    string LastItemSummary
);