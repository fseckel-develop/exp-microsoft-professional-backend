using KnapsackMemoizationDemo.Models;

namespace KnapsackMemoizationDemo.Algorithms;

public sealed class MemoizedKnapsackSolver : IKnapsackSolver
{
    private readonly Dictionary<(int ItemCount, int Capacity), int> _memo = [];
    private long _callCount;

    public string Name => "Memoized (Top-Down Dynamic Programming)";

    public KnapsackResult Solve(IReadOnlyList<SupplyItem> items, int capacity)
    {
        _memo.Clear();
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

        var key = (ItemCount: itemCount, Capacity: capacity);

        if (_memo.TryGetValue(key, out int cached))
            return cached;

        var current = items[itemCount - 1];

        int result;
        if (current.Weight > capacity)
        {
            result = SolveRecursive(items, capacity, itemCount - 1);
        }
        else
        {
            int includeValue =
                current.Value +
                SolveRecursive(items, capacity - current.Weight, itemCount - 1);

            int excludeValue =
                SolveRecursive(items, capacity, itemCount - 1);

            result = Math.Max(includeValue, excludeValue);
        }

        _memo[key] = result;
        return result;
    }
}