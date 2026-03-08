using System.Diagnostics;
using KnapsackMemoizationDemo.Algorithms;
using KnapsackMemoizationDemo.Models;

namespace KnapsackMemoizationDemo.Benchmarking;

public sealed class SolverBenchmark
{
    public SolverReport Run(
        IKnapsackSolver solver,
        IReadOnlyList<SupplyItem> items,
        int capacity)
    {
        var stopwatch = Stopwatch.StartNew();

        var result = solver.Solve(items, capacity);

        stopwatch.Stop();

        return new SolverReport(
            SolverName: solver.Name,
            BestValue: result.BestValue,
            CallCount: result.CallCount,
            ElapsedMilliseconds: stopwatch.Elapsed.TotalMilliseconds);
    }
}