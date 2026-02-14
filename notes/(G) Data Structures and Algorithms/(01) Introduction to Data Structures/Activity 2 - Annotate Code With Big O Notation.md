## Objective

-	Analyse code using Big O notation  
-	Evaluate time and space complexity  
-	Apply performance analysis to arrays, linked lists, stacks, and queues  

---
## A. Array Operations

-	Context:
	-	Accessing elements  
	-	Iterating through the array  

### Time and Space Analysis

-	Accessing an element:
	-	Code: numbers[2]  
	-	Time Complexity: O(1) – Constant  
	-	Space Complexity: O(1) – No additional memory used  
	-	Reason:
		-	Direct indexed access due to contiguous memory  

-	Looping through array:
	-	Code: for (int i = 0; i < numbers.Length; i++)  
	-	Time Complexity: O(n) – Linear  
	-	Space Complexity: O(1) – No extra storage  
	-	Reason:
		-	Each element is visited once  

-	Key Insight:
	-	Arrays provide constant-time access  
	-	Traversal always scales linearly  

---
## B. Linked List Operations

-	Context:
	-	Traversing a linked list  

### Time and Space Analysis

-	Traversal:
	-	Code: foreach (var num in list)  
	-	Time Complexity: O(n) – Linear  
	-	Space Complexity: O(1) – No extra memory used  
	-	Reason:
		-	Each node must be visited sequentially  
		-	No indexed access available  

-	Key Insight:
	-	Linked lists require sequential traversal  
	-	Accessing arbitrary elements is not O(1)  

---
## C. Stack and Queue Operations

-	Context:
	-	Push, Pop, Enqueue, Dequeue operations  

### Annotated Complexity

-	Stack:
	-	Push():
		-	Time: O(1) – Constant insertion  
		-	Reason: Operates only at the top  
	-	Pop():
		-	Time: O(1) – Constant removal  
		-	Reason: Removes only top element  

-	Queue:
	-	Enqueue():
		-	Time: O(1) – Constant insertion  
		-	Reason: Adds to rear only  
	-	Dequeue():
		-	Time: O(1) – Constant removal  
		-	Reason: Removes from front only  

### Performance Comparison

-	Insertion:
	-	Stack → O(1)  
	-	Queue → O(1)  

-	Deletion:
	-	Stack → O(1)  
	-	Queue → O(1)  

-	Access (non-front/top element):
	-	Stack → O(n)  
	-	Queue → O(n)  

-	Key Insight:
	-	Both structures are highly efficient for insertion and removal  
	-	Neither supports constant-time random access  

---
## D. Space Complexity Evaluation

-	Context:
	-	Storing n elements in an array and linked list  

### Space Analysis

-	Array:
	-	Space Complexity: O(n)  
	-	Memory Allocation:
		-	Preallocated contiguous block  
	-	Characteristics:
		-	Low overhead  
		-	Fixed size  

-	Linked List:
	-	Space Complexity: O(n)  
	-	Memory Allocation:
		-	Dynamic allocation per node  
	-	Characteristics:
		-	Additional memory for pointers  
		-	Flexible growth  

### Comparison of Storage Efficiency

-	Arrays:
	-	More memory-efficient (no pointer overhead)  
	-	Less flexible resizing  

-	Linked Lists:
	-	More flexible  
	-	Higher memory overhead due to node references  

---
## Key Takeaways

-	O(1) operations remain constant regardless of input size  
-	O(n) operations grow proportionally with input  
-	Arrays:
	-	O(1) access  
	-	O(n) traversal  
	-	O(n) space  

-	Linked Lists:
	-	O(n) traversal  
	-	O(1) insertion (with node reference)  
	-	O(n) space with additional pointer overhead  

-	Stacks and Queues:
	-	O(1) insertion and deletion  
	-	O(n) access to internal elements  

-	Applying Big O analysis ensures scalable and efficient system design  