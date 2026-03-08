using KnapsackMemoizationDemo.Algorithms;
using KnapsackMemoizationDemo.Benchmarking;
using KnapsackMemoizationDemo.Data;
using KnapsackMemoizationDemo.Presentation;

namespace KnapsackMemoizationDemo;

internal static class Program
{
    private static void Main()
    {
        const int capacity = 200;

        var items = SupplyItemFactory.CreateExpeditionSupplies();
        var writer = new ConsoleWriter();
        var benchmark = new SolverBenchmark();

        var solvers = new IKnapsackSolver[]
        {
            new RecursiveKnapsackSolver(),
            new MemoizedKnapsackSolver()
        };

        writer.WriteTitle("Knapsack Comparison Demo");

        writer.WriteSection(
            "Expedition Supplies",
            "Choose the most valuable set of supplies without exceeding the carrying capacity.");

        writer.WriteItems(items);
        writer.WriteCapacity(capacity);

        foreach (var solver in solvers)
        {
            var report = benchmark.Run(solver, items, capacity);
            writer.WriteReport(report);
        }

        writer.WriteComparisonNote();
    }
}