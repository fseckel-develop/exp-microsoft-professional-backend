using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        // -------------------------------
        // Basic Binary Search Example
        // -------------------------------
        int[] userIds = { 101, 203, 304, 405, 506, 607, 708, 809, 910 };
        int target = 506;

        int index = Search.BinarySearch(userIds, target);
        if (index != -1)
            Console.WriteLine($"User ID {target} found at index {index}.");
        else
            Console.WriteLine($"User ID {target} not found.");

        int targetNotFound = 999;
        index = Search.BinarySearch(userIds, targetNotFound);
        if (index != -1)
            Console.WriteLine($"User ID {targetNotFound} found at index {index}.");
        else
            Console.WriteLine($"User ID {targetNotFound} not found.");

        // -------------------------------
        // Performance Test
        // -------------------------------
        const int largeArraySize = 1_000_000_000; // 1 billion elements
        int[] largeDataset = new int[largeArraySize];
        for (int i = 0; i < largeDataset.Length; i++)
        {
            largeDataset[i] = i;
        }

        int largeTarget = 999_999_999;
        Stopwatch stopwatch = new Stopwatch();

        // Binary Search Performance
        stopwatch.Start();
        int binarySearchResult = Search.BinarySearch(largeDataset, largeTarget);
        stopwatch.Stop();
        Console.WriteLine($"Binary Search found item at index {binarySearchResult}");
        Console.WriteLine($"Binary Search Time: {stopwatch.ElapsedMilliseconds} ms");

        // Linear Search Performance
        stopwatch.Restart();
        int linearSearchResult = Search.LinearSearch(largeDataset, largeTarget);
        stopwatch.Stop();
        Console.WriteLine($"Linear Search found item at index {linearSearchResult}");
        Console.WriteLine($"Linear Search Time: {stopwatch.ElapsedMilliseconds} ms");
    }
}