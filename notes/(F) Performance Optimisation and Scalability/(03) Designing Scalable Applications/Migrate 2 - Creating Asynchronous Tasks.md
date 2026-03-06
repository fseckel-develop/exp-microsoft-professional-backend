>This note originated in the course 'Data Structures and Algorithms' where the instructors seemed to have misplaced it. Therefore it was moved in here into a more suitable chapter.

## Objective

- Implement a simple asynchronous task in .NET Core using `async` / `await`.
- Understand non-blocking behaviour in applications.
- Prepare for more advanced asynchronous programming patterns.

---
## Why Asynchronous Processing Matters

- Modern apps must handle multiple tasks simultaneously without becoming unresponsive.
- Asynchronous programming allows background operations without blocking main execution.
- Common use cases:
	- Fetching data from an API without freezing the UI
	- Running background processes without blocking user actions
	- Improving performance by handling multiple requests concurrently
- .NET Core uses `async` / `await` to implement asynchronous tasks efficiently.

---
## Example 1: Basic Asynchronous Task

**Scenario**:
- A program simulates a delay before completing a task.
- Demonstrates non-blocking execution.

**Implementation:**

```csharp
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Task starting...");
        
        // Call the asynchronous method
        await PerformTaskAsync();
        
        Console.WriteLine("Task completed!");
    }

    static async Task PerformTaskAsync()
    {
        Console.WriteLine("Processing...");
        
        // Simulate a 3-second delay
        await Task.Delay(3000);
        
        Console.WriteLine("Task finished.");
    }
}
````

**Justification:**
- `async Task Main(string[] args)` => enables the Main method to use await.
- `await PerformTaskAsync()` => waits for method to finish without blocking the main thread.
- `await Task.Delay(3000)` => simulates a 3-second background task.
- The program remains responsive during execution.
- Demonstrates true non-blocking behaviour.

---
## Example 2: Parallel Asynchronous Tasks

**Scenario**:
- A program fetches data from two APIs simultaneously.
- Demonstrates parallel execution.

**Implementation**:

```Csharp
using System;
using System.Threading.Tasks;

class Program
{
    static async Task FetchAPI1Async()
    {
        Console.WriteLine("Fetching API 1...");
        await Task.Delay(3000); // Simulate API delay
        Console.WriteLine("API 1 Data Retrieved.");
    }

    static async Task FetchAPI2Async()
    {
        Console.WriteLine("Fetching API 2...");
        await Task.Delay(2000); // Simulate API delay
        Console.WriteLine("API 2 Data Retrieved.");
    }

    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting API calls...");

        // Run both API calls at the same time
        Task task1 = FetchAPI1Async();
        Task task2 = FetchAPI2Async();

        await Task.WhenAll(task1, task2); // Wait for both to finish
        
        Console.WriteLine("All API calls completed.");
    }
}
```

**Justification:**
- `Task.WhenAll(task1, task2` => runs both tasks in parallel
- Each `Task.Delay(x)` simulates an API response delay.
- Total execution time is reduced compared to sequential execution.
- both API calls execute concurrently, improving efficiency and responsiveness.

---
## Example 3: Delayed Logging System

**Scenario**:
- A program logs events every 2 seconds.
- Simulates a background logging system.

**Implementation:**

```csharp
using System;
using System.Threading.Tasks;

class Program
{
    static async Task LogMessageAsync()
    {
        for (int i = 1; i <= 5; i++)
        {
            await Task.Delay(2000); // Logs every 2 seconds
            Console.WriteLine(
	            $"Log Entry {i}: Event recorded at {DateTime.Now}"
			);
        }
    }

    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting logging...");
        Task logTask = LogMessageAsync();

        Console.WriteLine("Logging in the background...");
        await logTask;

        Console.WriteLine("Logging complete.");
    }
}
```

**Justification**:
- The loop runs asynchronously.
- The main thread remains unblocked.
- Demonstrates background execution with maintained responsiveness.

---
## Example 4: Parallel API Calls

**Scenario**:
- A program retrieves data from multiple APIs simultaneously.
- Simulates parallel data-fetching.

**Implementation:**

```csharp
using System;
using System.Threading.Tasks;

class Program
{
    static async Task FetchAPI1Async()
    {
        Console.WriteLine("Fetching API 1...");
        await Task.Delay(3000); // Simulating API delay
        Console.WriteLine("API 1 Data Retrieved.");
    }

    static async Task FetchAPI2Async()
    {
        Console.WriteLine("Fetching API 2...");
        await Task.Delay(2000); // Simulating API delay
        Console.WriteLine("API 2 Data Retrieved.");
    }

    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting API calls...");
        await Task.WhenAll(FetchAPI1Async(), FetchAPI2Async());
        Console.WriteLine("All API calls completed.");
    }
}
```

**Justification**:
- Both API calls execute in parallel.
- `Task.WhenAll()` ensures efficient concurrency.
- Reduces total execution time compared to sequential execution.
- Demonstrates scalability benefits of asynchronous programming.

---