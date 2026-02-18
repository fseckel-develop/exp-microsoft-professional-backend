## Introduction

- SwiftCollab’s reporting dashboard generates large datasets for analytics.  
- Current sorting algorithm (**Bubble Sort**) is inefficient, leading to slow data processing for large workloads.  

- Objective:  
	- Analyse, optimise, and refactor the sorting algorithm using an LLM (e.g., MS Copilot)  
	- Improve **time and space efficiency**  
	- Support large-scale, real-time dataset processing  

---
## Step 1: Scenario Analysis

```C#
using System;
public class Sorting
{
	public static void BubbleSort(int[] arr)
	{
		for (int i = 0; i < arr.Length - 1; i++)
		{
			for (int j = 0; j < arr.Length - i - 1; j++)
			{
				if (arr[j] > arr[j + 1])
				{
					int temp = arr[j];
					arr[j] = arr[j + 1];
					arr[j + 1] = temp;
				}
			}
		}
	}
	
	public static void PrintArray(int[] arr)
	{
		foreach (var item in arr)
		{
			Console.Write(item + " ");
		}
		Console.WriteLine();
	}
	
	public static void Main()
	{
		int[] dataset = { 50, 20, 40, 10, 30 };
		Console.WriteLine("Before Sorting:");
		PrintArray(dataset);
		BubbleSort(dataset);
		Console.WriteLine("After Sorting:");
		PrintArray(dataset);
	}
}
```

- System must sort datasets efficiently for reporting and analytics.  
- Current implementation uses **Bubble Sort**, which has:  
	- Time complexity: O(n²)  
	- Poor scalability for large datasets  
	- Limited efficiency in real-time reporting  
- Goal:  
	- Replace with **Quick Sort** or **Merge Sort**  
	- Reduce time complexity to O(n log n)  
	- Optimise memory usage and support parallel execution  

---
## Step 2: Identified Inefficiencies

- Repeated comparisons and swaps in Bubble Sort cause O(n²) runtime  
- Not suitable for large datasets  
- Space complexity could be improved with in-place sorting (Quick Sort)  
- No support for parallel processing to utilise multi-core CPUs  

---
## Step 3: Optimised Sorting Algorithm Implementation

```csharp
using System;
using System.Threading.Tasks;

public class Sorting
{
    // Optimized Quick Sort implementation
    public static void QuickSort(int[] arr, int left, int right)
    {
        if (left < right)
        {
            int pivotIndex = Partition(arr, left, right);
            // Parallelize recursive calls for large datasets
            if (right - left > 1000) // Threshold for parallel execution
            {
                Parallel.Invoke(
                    () => QuickSort(arr, left, pivotIndex - 1),
                    () => QuickSort(arr, pivotIndex + 1, right)
                );
            }
            else
            {
                QuickSort(arr, left, pivotIndex - 1);
                QuickSort(arr, pivotIndex + 1, right);
            }
        }
    }

    private static int Partition(int[] arr, int left, int right)
    {
        int pivot = arr[right];
        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            if (arr[j] <= pivot)
            {
                i++;
                Swap(arr, i, j);
            }
        }
        Swap(arr, i + 1, right);
        return i + 1;
    }

    private static void Swap(int[] arr, int i, int j)
    {
        int temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }

    // Utility function to print arrays
    public static void PrintArray(int[] arr)
    {
        foreach (var item in arr)
            Console.Write(item + " ");
        Console.WriteLine();
    }

    public static void Main()
    {
        int[] dataset = { 50, 20, 40, 10, 30, 60, 15, 35 };
        Console.WriteLine("Before Sorting:");
        PrintArray(dataset);

        QuickSort(dataset, 0, dataset.Length - 1);

        Console.WriteLine("After Sorting:");
        PrintArray(dataset);
    }
}
````

---
## Step 4: Explanation of Improvements

- Quick Sort Implementation
	- Replaced **Bubble Sort** (O(n²)) with **Quick Sort** (O(n log n) average case)
	- Reduces sorting time significantly for large datasets

- In-Place Sorting
	- Quick Sort sorts the array **in-place**, reducing memory usage
	- Eliminates need for additional arrays unlike Merge Sort

- Parallel Execution
	- For large datasets, recursive calls are executed in parallel
	- Utilizes multi-core CPUs, reducing overall runtime

- Partitioning and Swap Optimisation
	- Efficient pivot-based partitioning ensures balanced splits
	- Reduces unnecessary comparisons and swaps

---
## Step 5: Reflection

- How did the LLM assist in refining the algorithm?
	- Suggested **Quick Sort** replacement for Bubble Sort
	- Recommended **in-place sorting** to optimise memory usage
	- Proposed **parallel execution** for large datasets

- Were any LLM-generated suggestions inaccurate or unnecessary?
	- All suggestions were valid
	- Parallelisation threshold was chosen heuristically; 
	  may require tuning for specific datasets

- What were the most impactful improvements implemented?
	- Time complexity reduction from O(n²) → O(n log n)
	- In-place sorting for reduced memory footprint
	- Parallel execution to leverage multiple cores

---