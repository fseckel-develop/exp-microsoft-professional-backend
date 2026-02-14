## Objective 

-	Analyse and compare arrays, linked lists, stacks, and queues  
-	Evaluate characteristics, performance trade-offs, and use cases  
-	Determine appropriate real-world applications  

---
## Arrays vs. Linked Lists

-	Both store sequential data but differ in memory model and performance behavior  
-	Key comparison dimensions:
	-	Memory allocation  
	-	Access time  
	-	Insertion and deletion cost  
	-	Practical use cases  

### Characteristics

-	Arrays:
	-	Stored in contiguous memory  
	-	Direct index-based access  
	-	Access time: O(1)  
	-	Insertion/Deletion: O(n) (due to shifting, except at end)  
	-	Best when size is fixed and frequent indexing is required  

-	Linked Lists:
	-	Nodes connected via pointers  
	-	Dynamic memory allocation  
	-	Access time: O(n) (sequential traversal required)  
	-	Insertion/Deletion: O(1) if node reference is known  
	-	Best when structure size changes frequently  

### Performance Trade-Offs

-	Arrays:
	-	Fast lookups  
	-	Less flexible resizing  
	-	Memory-efficient (no pointer overhead)  

-	Linked Lists:
	-	Slower lookups  
	-	Highly flexible structure  
	-	Additional memory required for pointers  

### Reflection Summary (Core Insight)

-	Arrays are preferable when:
	-	Random access is required  
	-	Data size is predictable  
	-	Example: database indexing, caching systems  

-	Linked lists are preferable when:
	-	Frequent insertions and deletions occur  
	-	Structure size is dynamic  
	-	Example: undo history, file systems  

-	Conclusion:
	-	Arrays optimize access speed  
	-	Linked lists optimize structural flexibility  
	-	The best choice depends on whether access efficiency or modification flexibility is prioritized  

---
## Stacks vs. Queues

-	Both are abstract data types with specific ordering principles  
-	Primary distinction:
	-	Order in which elements are removed  

### Characteristics

-	Stacks (LIFO – Last In, First Out):
	-	Push (insert): O(1)  
	-	Pop (remove): O(1)  
	-	Access deeper elements: O(n)  
	-	Best for reversing order or nested operations  

-	Queues (FIFO – First In, First Out):
	-	Enqueue (insert): O(1)  
	-	Dequeue (remove): O(1)  
	-	Access deeper elements: O(n)  
	-	Best for sequential and fair processing  

### Performance Trade-Offs

-	Both structures:
	-	Constant-time insertion and removal  
	-	Linear-time access to non-front/top elements  

-	Difference lies in logical behavior:
	-	Stack prioritizes most recent element  
	-	Queue prioritizes earliest element  

### Reflection Summary (Core Insight)

-	Stacks are preferable when:
	-	Recent actions must be reversed  
	-	Nested processes are handled  
	-	Example: undo functionality, recursion, call stacks  

-	Queues are preferable when:
	-	Tasks must be processed in arrival order  
	-	System fairness and load management are required  
	-	Example: API request handling, task scheduling, print queues  

-	Conclusion:
	-	Stacks support backtracking and reversal logic  
	-	Queues support structured, orderly task management  
	-	Choosing the correct structure ensures system efficiency and clarity of logic  