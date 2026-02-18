## Introduction

- SwiftCollab is a growing collaboration and workflow automation platform  
- Task assignment system relies on a binary tree for priority-based task storage  
- Current issues:  
	- Slow task retrieval as system scales  
	- Tree becomes unbalanced over time  
	- No efficient search functionality  
	- Poor scalability under high load  

- Objective:  
	- Refine and optimise the binary tree implementation  
	- Improve performance, scalability, and maintainability  
	- Apply LLM-assisted insights for optimisation  

---
## Step 1: Scenario Analysis

```C#
public class Node
{
	public int Value;
	public Node Left, Right;
	
	public Node(int value)
	{
		Value = value;
		Left = Right = null;
	}
}

public class BinaryTree
{
	public Node Root;
	
	public void Insert(int value)
	{
		if (Root == null)
			Root = new Node(value);
		else
			InsertRecursive(Root, value);
	}
	
	private void InsertRecursive(Node current, int value)
	{
		if (value < current.Value)
		{
			if (current.Left == null)
				current.Left = new Node(value);
			else
				InsertRecursive(current.Left, value);
		}
		else
		{
			if (current.Right == null)
				current.Right = new Node(value);
			else
				InsertRecursive(current.Right, value);
		}
	}
	
	public void PrintInOrder(Node node)
	{
		if (node == null) return;
		PrintInOrder(node.Left);
		Console.Write(node.Value + " ");
		PrintInOrder(node.Right);
	}
}
```

- Existing implementation problems:  
	- Tree is not self-balancing → can degrade to O(n) time complexity  
	- Missing dedicated search method  
	- Recursive logic is fragmented and inefficient  
	- Code structure is malformed and unclear  

- Performance risks:  
	- Unbalanced tree behaves like a linked list  
	- Slow insert and search operations  
	- Increased latency in task prioritisation  

---
## Step 2: Identified Areas for Improvement

- Structural Corrections  
	- Fix malformed class definitions  
	- Separate `Node` and `BinaryTree` clearly  
	- Ensure proper recursive insertion logic  

- Tree Balancing  
	- Convert basic Binary Search Tree (BST) to self-balancing tree  
	- Use AVL balancing strategy  
	- Maintain O(log n) insert and search complexity  

- Search Functionality  
	- Add efficient `Search(int value)` method  
	- Implement iterative or optimized recursive lookup  

- Recursive Optimization  
	- Remove redundant recursive calls  
	- Centralize insertion logic  
	- Use height tracking to avoid recomputation  

- Memory & Performance  
	- Store height inside node to avoid recalculating  
	- Minimize unnecessary object creation  
	- Keep recursion depth controlled via balancing  

---
## Step 3: Optimised Binary Tree Implementation (AVL Tree)

```csharp
using System;

public class Node
{
    public int Value;
    public Node Left;
    public Node Right;
    public int Height;

    public Node(int value)
    {
        Value = value;
        Height = 1;
    }
}

public class BinaryTree
{
    public Node Root;

    // Get height safely
    private int GetHeight(Node node)
    {
        return node?.Height ?? 0;
    }

    // Get balance factor
    private int GetBalance(Node node)
    {
        return node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);
    }

    // Right rotation (AVL balancing)
    private Node RotateRight(Node y)
    {
        Node x = y.Left;
        Node T2 = x.Right;

        x.Right = y;
        y.Left = T2;

        y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
        x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

        return x;
    }

    // Left rotation (AVL balancing)
    private Node RotateLeft(Node x)
    {
        Node y = x.Right;
        Node T2 = y.Left;

        y.Left = x;
        x.Right = T2;

        x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
        y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

        return y;
    }

    // Insert with AVL balancing
    public void Insert(int value)
    {
        Root = InsertRecursive(Root, value);
    }

    private Node InsertRecursive(Node node, int value)
    {
        if (node == null)
            return new Node(value);

        if (value < node.Value)
            node.Left = InsertRecursive(node.Left, value);
        else if (value > node.Value)
            node.Right = InsertRecursive(node.Right, value);
        else
            return node; // Prevent duplicates

        node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

        int balance = GetBalance(node);

        // Left Left Case
        if (balance > 1 && value < node.Left.Value)
            return RotateRight(node);

        // Right Right Case
        if (balance < -1 && value > node.Right.Value)
            return RotateLeft(node);

        // Left Right Case
        if (balance > 1 && value > node.Left.Value)
        {
            node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }

        // Right Left Case
        if (balance < -1 && value < node.Right.Value)
        {
            node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }

        return node;
    }

    // Efficient search method (O(log n))
    public bool Search(int value)
    {
        Node current = Root;

        while (current != null)
        {
            if (value == current.Value)
                return true;
            else if (value < current.Value)
                current = current.Left;
            else
                current = current.Right;
        }

        return false;
    }
    
    private void PrintInOrder(Node node)
    {
		if (node == null)
			return;
		PrintInOrder(node.Left);
		Console.Write(node.Value + " ");
		PrintInOrder(node.Right);
    }
}
````

---
## Step 4: Explanation of Improvements

- Converted basic BST → AVL self-balancing tree
    - Guarantees O(log n) insertion and search
    - Prevents tree degradation

- Added height tracking in each node
    - Avoids recalculating subtree height repeatedly
    - Improves efficiency

- Implemented rotation methods
    - Left rotation
    - Right rotation
    - Double rotations for imbalance correction

- Added iterative search method
    - Avoids unnecessary recursive overhead
    - Improves performance for frequent lookups

- Prevented duplicate entries
    - Ensures clean task prioritisation

---
## Step 5: Performance Comparison

- Before Optimisation
    - Worst-case time complexity: O(n)
    - Tree could become skewed
    - Slow retrieval under heavy load

- After Optimisation
    - Guaranteed O(log n) insert and search
    - Balanced structure maintained automatically
    - Scales efficiently as tasks increase

---
## Step 6: Reflection

- How did the LLM assist in refining the code?
    - Identified structural issues in the original implementation
    - Recommended converting to a self-balancing AVL tree
    - Suggested adding height tracking and rotation logic
    - Improved search efficiency with iterative traversal

- Were any LLM-generated suggestions inaccurate or unnecessary?
	- PrintInOrder() method first was missing in the suggestion
    - Full balancing (AVL) may be unnecessary for very small datasets
    - For extremely high-scale systems, alternative structures like heaps or priority queues might be more appropriate

- Most impactful improvements implemented
    - Self-balancing logic (AVL rotations)
    - Height caching inside nodes
    - Efficient search method
    - Clean, maintainable code structure

---