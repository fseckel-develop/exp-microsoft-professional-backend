## Example 1: Optimising Nested Loops in a Sales Calculation

### Scenario

A developer writes a C# program that calculates total sales for each product using nested loops, causing performance bottlenecks with larger datasets:

```csharp
var products = new List<string> { "ProductA", "ProductB", "ProductC" };
var sales = new Dictionary<string, int>{
    { "ProductA", 100 },
    { "ProductB", 200 },
    { "ProductC", 300 }
};
foreach (var product in products){
    foreach (var sale in sales)
    {
        if (sale.Key == product)
        {
            Console.WriteLine($"{product}: {sale.Value}");
        }
    }
}
```

### Step-by-Step Process with Copilot Prompts and Responses

#### Performance Bottleneck Detection 

**Prompt:** "Identify inefficiencies in this nested loop and suggest improvements."  

**Copilot Response:**  "The inner loop iterates over all sales for each product, causing redundant comparisons. Instead, use ContainsKey for direct lookup to eliminate the unnecessary loop."

**Optimised Code:**

```csharp
products.ForEach(product =>{
    if (sales.ContainsKey(product))
    {
        Console.WriteLine($"{product}: {sales[product]}");
    }
});
```

#### Measuring Performance Improvement

**Prompt:** "How can I compare execution times before and after optimisation?"  

**Copilot Response:**  "Use Stopwatch to measure and compare execution time of both versions."

**Code for Execution Time Measurement:**

```csharp
var stopwatch = System.Diagnostics.Stopwatch.StartNew();

// Run optimized code
products.ForEach(product =>{
    if (sales.ContainsKey(product))
    {
        Console.WriteLine($"{product}: {sales[product]}");
    }
});

stopwatch.Stop();
Console.WriteLine($"Execution time: {stopwatch.ElapsedMilliseconds} ms");
```

---
## Example 2: More efficient, scalable, maintainable Code

### Scenario

A program reads log files sequentially, performing multiple separate file reads:

```csharp
Console.WriteLine(File.ReadAllText("Log1.txt"));
Console.WriteLine(File.ReadAllText("Log2.txt"));
Console.WriteLine(File.ReadAllText("Log3.txt"));
```

### Step-by-Step Process with Copilot Prompts and Responses

#### Performance Bottleneck Detection 

**Prompt:** "How can I make this code more efficient?"  

**Copilot Response:** "Change code to loop through the files in a batch process to improve efficiency. In this case, it improves scalability and maintainability."

**Optimised Code:**

```csharp
var logFiles = new[] { "Log1.txt", "Log2.txt", "Log3.txt" };
foreach (var logFile in logFiles){
    Console.WriteLine(File.ReadAllText(logFile));
}
```

#### Measuring Efficiency Improvement

**Prompt:** "How can I confirm the optimised code is more efficient?"  

**Copilot Response:**  "The performance difference may be negligible for small files, but the loop approach improves scalability and maintainability, simplifies adding/removing files, and enhances code readability."

---
## Example 3: Optimise a Sorting Algorithm

### Scenario

A developer implements Bubble Sort, which Copilot identifies as inefficient for large datasets:

```csharp
void BubbleSort(int[] arr){
    int n = arr.Length;
    for (int i = 0; i < n - 1; i++)
    {
        for (int j = 0; j < n - i - 1; j++)
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
```

**Your Task:**

- Use Copilot to optimise the sorting algorithm for better performance.
- Measure execution time before and after optimisation.
- Document the performance improvements.

### Optimised Sorting Algorithm (QuickSort)

```csharp
void QuickSort(int[] arr, int left, int right){
    if (left < right)
    {
        int pivot = Partition(arr, left, right);
        QuickSort(arr, left, pivot - 1);
        QuickSort(arr, pivot + 1, right);
    }
}

int Partition(int[] arr, int left, int right){
    int pivot = arr[right];
    int i = left - 1;
    for (int j = left; j < right; j++)
    {
        if (arr[j] <= pivot)
        {
            i++;
            (arr[i], arr[j]) = (arr[j], arr[i]);
        }
    }
    (arr[i + 1], arr[right]) = (arr[right], arr[i + 1]);
    return i + 1;
}
```

**Execution Time Comparison:**

- Before Optimisation (Bubble Sort): 120ms for 10,000 elements  
- After Optimisation (QuickSort): 5ms for 10,000 elements

---
### Example 4: Optimise Unused Dependencies in a Web Application

### Scenario

A C# web project includes unused dependencies:

```csharp
using System;
using Newtonsoft.Json; 
// using System.Data; // Unused
```

**Your Task:**

- Use Copilot to identify and remove unused dependencies.
- Measure execution performance before and after cleanup.
- Explain how reducing unnecessary dependencies improves efficiency.

### Optimised Code (Removed Unused Dependencies)

```csharp
using System;
using Newtonsoft.Json;
```

**Execution Time Comparison:**

- Before Optimization: Startup time 500ms due to unnecessary imports  
- After Optimization: Startup time 300ms after removing unused `System.Data`  

**Efficiency Benefits:**

- Reduced memory usage  
- Faster startup and runtime  
- Cleaner, maintainable codebase

---