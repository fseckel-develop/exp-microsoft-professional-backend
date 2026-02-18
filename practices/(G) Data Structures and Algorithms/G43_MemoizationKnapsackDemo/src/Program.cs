using System.Diagnostics;

class Program
{
    static void Main()
    {
        int[] values =
        {
            60, 100, 120, 200, 150, 180, 75, 90, 110, 95, 130, 170, 85, 140, 
            160, 210, 55, 125, 145, 190, 155, 175, 115, 80, 135, 165, 105, 195
        };
        int[] weights =
        {
            10, 20, 30, 40, 25, 35, 15, 22, 28, 18, 33, 27, 12, 24, 
            31, 45, 8, 26, 29, 38, 21, 34, 17, 14, 23, 32, 19, 36
        };
        int capacity = 200;

        Console.WriteLine("=== Knapsack Problem – Recursive vs Memoized ===\n");
        Stopwatch stopwatch = new Stopwatch();

        // Recursive Knapsack Version
        stopwatch.Start();
        int recursiveResult = Knapsack.KnapsackRecursive(values, weights, capacity, values.Length);
        stopwatch.Stop();
        Console.WriteLine("Recursive Knapsack Solver:");
        Console.WriteLine($"  Result    : {recursiveResult,10:N0}");
        Console.WriteLine($"  Time      : {stopwatch.Elapsed.TotalMilliseconds,10:F4} ms");
        Console.WriteLine($"  CallCount : {Knapsack.RecursiveCallCounter,10:N0}\n");

        // Memoized Knapsack Version
        stopwatch.Restart();
        int memoizedResult = Knapsack.KnapsackMemoized(values, weights, capacity, values.Length);
        stopwatch.Stop();
        Console.WriteLine("Memoized Knapsack Solver:");
        Console.WriteLine($"  Result    : {memoizedResult,10:N0}");
        Console.WriteLine($"  Time      : {stopwatch.Elapsed.TotalMilliseconds,10:F4} ms");
        Console.WriteLine($"  CallCount : {Knapsack.MemoizedCallCounter,10:N0}");
    }
}