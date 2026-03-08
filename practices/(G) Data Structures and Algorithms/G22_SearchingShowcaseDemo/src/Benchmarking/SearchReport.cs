namespace SearchingAlgorithmsDemo.Benchmarking;

public sealed record SearchReport(
    string Algorithm,
    string Scenario,
    long ElapsedMilliseconds,
    int FoundIndex,
    bool WasFound,
    string? ItemSummary
);