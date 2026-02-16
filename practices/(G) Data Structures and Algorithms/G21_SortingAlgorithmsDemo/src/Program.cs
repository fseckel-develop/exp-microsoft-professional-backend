using System.Diagnostics;

class Program
{
    static void Main()
    {
        Stopwatch stopwatch = new Stopwatch();
        Random rand = new Random();
        int[] numbers = Enumerable.Range(1, 50000).OrderBy(x => rand.Next()).ToArray();
        
        // QuickSort Demonstration:
        int[] quicksortArray = (int[])numbers.Clone();
        stopwatch.Start();
        Sort.QuickSort(quicksortArray, 0, quicksortArray.Length - 1);
        stopwatch.Stop();
        Console.WriteLine("Quicksort Time: " + stopwatch.ElapsedMilliseconds + " ms");

        // MergeSort Demonstration:
        int[] mergeSortArray = (int[])numbers.Clone();
        stopwatch.Restart();
        Sort.MergeSort(mergeSortArray, 0, mergeSortArray.Length - 1);
        stopwatch.Stop();
        Console.WriteLine("Merge Sort Time: " + stopwatch.ElapsedMilliseconds + " ms");

        // BubbleSort Demonstration:
        int[] bubbleSortArray = (int[])numbers.Clone();
        stopwatch.Restart();
        Sort.BubbleSort(bubbleSortArray);
        stopwatch.Stop();
        Console.WriteLine("Bubble Sort Time: " + stopwatch.ElapsedMilliseconds + " ms");
    }
}