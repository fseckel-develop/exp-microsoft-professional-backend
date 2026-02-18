## Introduction

- SwiftCollab handles API requests through a workflow automation engine  
- Current system processes requests sequentially, causing delays under heavy traffic  
- High-priority tasks (e.g., authentication, health checks) must be processed first  
- Existing implementation uses a **List** with `Sort()` on every enqueue  
	- Inefficient: O(n log n) insertion  
	- Dequeue operation is O(n)  

- Objective:  
	- Refine the priority queue using a Min-Heap (binary heap)  
	- Improve insertion and dequeue efficiency  
	- Support batch enqueue and thread-safe operations  
	- Apply LLM-generated insights to optimise the algorithm  

---
## Step 1: Scenario Analysis

```C#
using System;
using System.Collections.Generic;

public class ApiRequest
{
	public string Endpoint { get; set; }
	public int Priority { get; set; }
	
	public ApiRequest(string endpoint, int priority)
	{
		Endpoint = endpoint;
		Priority = priority;
	}
}

public class ApiRequestQueue
{
	private List<ApiRequest> requests = new List<ApiRequest>();
	
	public void Enqueue(ApiRequest request)
	{
		requests.Add(request);
		requests.Sort((a, b) => a.Priority.CompareTo(b.Priority)); 
		// Inefficient sorting
	}
	
	public ApiRequest Dequeue()
	{
		if (requests.Count == 0)
			return null;
		ApiRequest nextRequest = requests[0];
		requests.RemoveAt(0);
		return nextRequest;
	}
}

class Program
{
	static void Main()
	{
		ApiRequestQueue queue = new ApiRequestQueue();
		queue.Enqueue(new ApiRequest("/auth", 1));
		queue.Enqueue(new ApiRequest("/data", 3));
		queue.Enqueue(new ApiRequest("/healthcheck", 2));
		Console.WriteLine($"Processing: {queue.Dequeue()?.Endpoint}");
	}
}
```

- System must prioritise critical API requests  
- Current issues:
	- Sorting on every enqueue is slow under high load  
	- Sequential processing is a bottleneck  
	- Not thread-safe for concurrent request handling  
- Goal:
	- Implement a **Min-Heap** based priority queue  
	- Reduce enqueue/dequeue complexity to O(log n)  
	- Support batch processing and concurrency  

---
## Step 2: Identified Areas for Improvement

- Replace `List.Sort()` with **binary heap**  
- Refactor `Enqueue()` to run in O(log n)  
- Refactor `Dequeue()` to remove min element in O(log n)  
- Add support for **bulk enqueue**  
- Ensure **thread safety** for concurrent access  

---
## Step 3: Optimized Priority Queue Implementation (Min-Heap)

```csharp
using System;
using System.Collections.Generic;
using System.Threading;

public class ApiRequest
{
    public string Endpoint { get; set; }
    public int Priority { get; set; }

    public ApiRequest(string endpoint, int priority)
    {
        Endpoint = endpoint;
        Priority = priority;
    }
}

// Thread-safe Min-Heap priority queue
public class ApiRequestQueue
{
    private List<ApiRequest> heap = new List<ApiRequest>();
    private readonly object lockObj = new object();

    public int Count => heap.Count;

    // Enqueue single request
    public void Enqueue(ApiRequest request)
    {
        lock (lockObj)
        {
            heap.Add(request);
            HeapifyUp(heap.Count - 1);
        }
    }

    // Enqueue multiple requests (bulk support)
    public void EnqueueBulk(IEnumerable<ApiRequest> requests)
    {
        lock (lockObj)
        {
            foreach (var req in requests)
                heap.Add(req);

            for (int i = (heap.Count / 2) - 1; i >= 0; i--)
                HeapifyDown(i);
        }
    }

    // Dequeue the highest priority request
    public ApiRequest Dequeue()
    {
        lock (lockObj)
        {
            if (heap.Count == 0)
                return null;

            ApiRequest root = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            HeapifyDown(0);

            return root;
        }
    }

    // Maintain heap property upwards
    private void HeapifyUp(int index)
    {
        while (index > 0)
        {
            int parent = (index - 1) / 2;
            if (heap[index].Priority >= heap[parent].Priority) break;

            Swap(index, parent);
            index = parent;
        }
    }

    // Maintain heap property downwards
    private void HeapifyDown(int index)
    {
        int smallest = index;
        int left = 2 * index + 1;
        int right = 2 * index + 2;

        if (left < heap.Count && heap[left].Priority < heap[smallest].Priority)
            smallest = left;
        if (right < heap.Count && heap[right].Priority < heap[smallest].Priority)
            smallest = right;

        if (smallest != index)
        {
            Swap(index, smallest);
            HeapifyDown(smallest);
        }
    }

    private void Swap(int i, int j)
    {
        var temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }

    // Peek at highest priority request without removing
    public ApiRequest Peek()
    {
        lock (lockObj)
        {
            return heap.Count > 0 ? heap[0] : null;
        }
    }
}

class Program
{
	static void Main()
	{
		ApiRequestQueue queue = new ApiRequestQueue();
		queue.Enqueue(new ApiRequest("/auth", 1));
		queue.Enqueue(new ApiRequest("/data", 3));
		queue.Enqueue(new ApiRequest("/healthcheck", 2));
		Console.WriteLine($"Processing: {queue.Dequeue()?.Endpoint}");
	}
}
````

---
## Step 4: Explanation of Improvements

- Min-Heap Implementation
	- Replaced inefficient `List.Sort()` with **binary heap**
	- **Enqueue complexity** reduced to O(log n)
	- **Dequeue complexity** reduced to O(log n)
	- Preserves correct priority ordering automatically

- Bulk Processing
	- Added `EnqueueBulk(IEnumerable< ApiRequest >)`
	- Efficiently adds multiple requests and restores heap property
	- Reduces repeated heapify operations

- Thread-Safe Handling
	- Added `lockObj` for concurrency control
	- Ensures safe multi-threaded enqueue/dequeue operations

- Heapify Logic
	- `HeapifyUp` ensures newly inserted nodes maintain min-heap property
	- `HeapifyDown` maintains heap property after dequeue or bulk insert
	- Avoids repeated sorting of the entire list

---
## Step 5: Reflection

- How did the LLM assist in refining the algorithm?
	- Recommended switching to a **Min-Heap** for priority queue
	- Suggested bulk insertion and thread-safe access
	- Highlighted performance gains for high-volume API request processing

- Were any LLM-generated suggestions inaccurate or unnecessary?
	- All suggestions were valid for SwiftCollab’s requirements
	- Thread-safety could be simplified using ConcurrentPriorityQueue in .NET 7+, 
	  but custom lock implementation is acceptable for clarity

- What were the most impactful improvements implemented?
	- Min-Heap implementation (O(log n) insert/dequeue)
	- Bulk enqueue support
	- Thread-safe operations
	- Heapify logic for efficient priority maintenance

---