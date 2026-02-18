using System.Collections.Generic;

public static class Knapsack
{
    private static Dictionary<string, int> memo = new Dictionary<string, int>();
    public static long RecursiveCallCounter { get; private set; } = 0;
    public static long MemoizedCallCounter { get; private set; } = 0;

    // Recursive Version (Brute Force)
    public static int KnapsackRecursive(int[] values, int[] weights, int capacity, int n)
    {
        RecursiveCallCounter++;

        if (n == 0 || capacity == 0)
        {
            return 0;
        }
            
        if (weights[n - 1] > capacity)
        {
            return KnapsackRecursive(values, weights, capacity, n - 1);
        }
            
        int includeItem = values[n - 1] + KnapsackRecursive(values, weights, capacity - weights[n - 1], n - 1);
        int excludeItem = KnapsackRecursive(values, weights, capacity, n - 1);
        return Math.Max(includeItem, excludeItem);
    }

    // Memoized Version (Top-Down Dynamic Programming)
    public static int KnapsackMemoized(int[] values, int[] weights, int capacity, int n)
    {
        MemoizedCallCounter++;
        
        if (n == 0 || capacity == 0)
        {
            return 0;
        }

        string key = $"{n}|{capacity}";
        if (memo.ContainsKey(key))
        {
            return memo[key];
        }
            
        if (weights[n - 1] > capacity)
        {
            memo[key] = KnapsackMemoized(values, weights, capacity, n - 1);
            return memo[key];
        }

        int includeItem = values[n - 1] + KnapsackMemoized(values, weights, capacity - weights[n - 1], n - 1);
        int excludeItem = KnapsackMemoized(values, weights, capacity, n - 1);
        memo[key] = Math.Max(includeItem, excludeItem);

        return memo[key];
    }
}