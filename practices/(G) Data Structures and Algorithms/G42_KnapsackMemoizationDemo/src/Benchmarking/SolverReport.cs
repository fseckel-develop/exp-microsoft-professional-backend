namespace KnapsackMemoizationDemo.Benchmarking;

public sealed record SolverReport(
    string SolverName,
    int BestValue,
    long CallCount,
    double ElapsedMilliseconds
);