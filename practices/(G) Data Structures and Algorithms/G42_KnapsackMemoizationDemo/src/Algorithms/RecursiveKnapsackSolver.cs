using KnapsackMemoizationDemo.Models;

namespace KnapsackMemoizationDemo.Algorithms;

public sealed class RecursiveKnapsackSolver : IKnapsackSolver
{
    private long _callCount;

    public string Name => "Recursive (Brute Force)";

    public KnapsackResult Solve(IReadOnlyList<SupplyItem> items, int capacity)
    {
        _callCount = 0;

        int bestValue = SolveRecursive(items, capacity, items.Count);

        return new KnapsackResult(
            BestValue: bestValue,
            CallCount: _callCount);
    }

    private int SolveRecursive(IReadOnlyList<SupplyItem> items, int capacity, int itemCount)
    {
        _callCount++;

        if (itemCount == 0 || capacity == 0)
            return 0;

        var current = items[itemCount - 1];

        if (current.Weight > capacity)
            return SolveRecursive(items, capacity, itemCount - 1);

        int includeValue =
            current.Value +
            SolveRecursive(items, capacity - current.Weight, itemCount - 1);

        int excludeValue =
            SolveRecursive(items, capacity, itemCount - 1);

        return Math.Max(includeValue, excludeValue);
    }
}