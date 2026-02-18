## Introduction

- SwiftCollab’s task execution system sometimes fails to process tasks due to **logic errors** and **lack of exception handling**.  
- These issues cause task failures, inconsistent performance, and system disruptions.  

- Objective:  
	- Use an LLM (e.g., Microsoft Copilot) to **debug and optimize** the task execution code  
	- Implement robust **exception handling**, logging, and retry mechanisms  
	- Ensure the system processes tasks reliably and efficiently  

---
## Step 1: Scenario Analysis

```C#
using System;
using System.Collections.Generic;

public class TaskExecutor
{
	private Queue<string> taskQueue = new Queue<string>();
	
	public void AddTask(string task)
	{
		taskQueue.Enqueue(task);
	}
	
	public void ProcessTasks()
	{
		while (taskQueue.Count > 0)
		{
			string task = taskQueue.Dequeue();
			Console.WriteLine($"Processing task: {task}");
			ExecuteTask(task);
		}
	}
	
	private void ExecuteTask(string task)
	{
		if (task == null)
		{
			throw new Exception("Task is null");
		}
		if (task.Contains("Fail"))
		{
			throw new Exception("Task execution failed");
		}
		Console.WriteLine($"Task {task} completed successfully.");
	}
}

class Program
{
	static void Main()
	{
		TaskExecutor executor = new TaskExecutor();
		executor.AddTask("Task 1");
		executor.AddTask(null); // This will cause a crash
		executor.AddTask("Fail Task"); // This will also cause a crash
		executor.AddTask("Task 2");
		executor.ProcessTasks();
	}
}
```

- Current task processing code is prone to crashes when:  
	- A task is `null`  
	- A task explicitly fails (e.g., contains `"Fail"`)  
- Goal:  
	- Prevent crashes  
	- Track errors systematically  
	- Retry failed tasks in a controlled manner  

---
## Step 2: Identified Problems

- **No null checks**, causing runtime exceptions  
- **Unhandled exceptions**, crashing the system  
- Lack of **logging** for monitoring failures  
- No **retry mechanism** for transient failures  

---
## Step 3: Optimised Task Execution Implementation

```csharp
using System;
using System.Collections.Generic;

public class TaskExecutor
{
    private Queue<string> taskQueue = new Queue<string>();

    // Adds a task to the queue
    public void AddTask(string task)
    {
        if (string.IsNullOrWhiteSpace(task))
        {
            Console.WriteLine("Warning: Ignored null or empty task.");
            return;
        }
        taskQueue.Enqueue(task);
    }

    // Processes all tasks in the queue
    public void ProcessTasks()
    {
        while (taskQueue.Count > 0)
        {
            string task = taskQueue.Dequeue();
            try
            {
                Console.WriteLine($"Processing task: {task}");
                ExecuteTask(task);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                $"Error executing task '{task}': {ex.Message}");
                // Retry logic: requeue failed task once
                if (!task.Contains("FailRetry"))
                {
                    taskQueue.Enqueue(task + "FailRetry");
                }
            }
        }
    }

    // Executes a single task
    private void ExecuteTask(string task)
    {
        if (task == null)
        {
            throw new ArgumentNullException(nameof(task), "Task is null");
        }

        // Simulate task failure
        if (task.Contains("Fail"))
        {
            throw new Exception("Task execution failed");
        }

        Console.WriteLine($"Task '{task}' completed successfully.");
    }
}

class Program
{
    static void Main()
    {
        TaskExecutor executor = new TaskExecutor();

        // Adding tasks, including problematic ones
        executor.AddTask("Task 1");
        executor.AddTask(null); // Ignored gracefully
        executor.AddTask("Fail Task"); // Will trigger retry
        executor.AddTask("Task 2");

        executor.ProcessTasks();
    }
}
````

---
## Step 3: Explanation of Improvements

 - Exception Handling
	- Added **try-catch** around task execution to prevent system crashes
	- Captures errors and logs them instead of terminating the process

- Null Checks
	- AddTask ignores null or empty tasks
	- Prevents runtime exceptions and unnecessary task failures

- Logging
	- All errors and warnings are printed with descriptive messages
	- Provides visibility into system behaviour without crashing

- Retry Logic
	- Failed tasks are re-enqueued once with a "FailRetry" suffix
	- Allows transient failures to be retried without infinite loops    

---
## Step 4: Reflection

- How did the LLM assist in debugging and optimising the code?
	- Suggested **null checks** and **exception handling**
	- Recommended adding **logging** for better monitoring
	- Proposed **simple retry logic** for failed tasks

- Were any LLM-generated suggestions inaccurate or unnecessary?
	- All suggestions were valid and appropriate for learners’ skill level
	- No complex concurrency or advanced task management was included to keep it simple

- What were the most impactful improvements implemented?
	- Prevented crashes by handling exceptions
    - Improved system reliability with null checks and logging
	- Introduced retry logic to enhance task completion success

---