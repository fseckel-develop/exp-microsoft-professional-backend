class Graph
{
    private Dictionary<int, List<int>> adjacencyList;

    public Graph()
    {
        adjacencyList = new Dictionary<int, List<int>>();
    }

    public void AddEdge(int source, int destination, bool directed = false)
    {
        if (!adjacencyList.ContainsKey(source))
        {
            adjacencyList[source] = new List<int>();
        }

        adjacencyList[source].Add(destination);

        if (directed) return; // Directed graph, so no reverse edge is added

        if (!adjacencyList.ContainsKey(destination))
        {
            adjacencyList[destination] = new List<int>();
        }

        adjacencyList[destination].Add(source); // Undirected graph
    }

    public void PrintAdjacencyList()
    {
        foreach (var kvp in adjacencyList)
        {
            Console.Write(kvp.Key + " -> ");
            Console.WriteLine(string.Join(", ", kvp.Value));
        }
        Console.WriteLine();
    }

    public void DepthFirstSearch(int start, bool iterative = false)
    {
        if (iterative)
        {
            DFSIterative(start, new HashSet<int>());
        }
        else
        {
            DFSRecursive(start, new HashSet<int>());
        }
        Console.WriteLine();
    }

    private void DFSIterative(int start, HashSet<int> visited)
    {
        Stack<int> stack = new Stack<int>();
        stack.Push(start);

        while (stack.Count > 0)
        {
            int node = stack.Pop();

            if (!visited.Contains(node))
            {
                Console.Write(node + " ");
                visited.Add(node);

                // Push neighbors in reverse order if goal is
                // natural ordering similar to recursive DFS
                if (adjacencyList.ContainsKey(node))
                {
                    for (int i = adjacencyList[node].Count - 1; i >= 0; i--)
                        stack.Push(adjacencyList[node][i]);
                }
            }
        }
    }

    private void DFSRecursive(int node, HashSet<int> visited)
    {
        if (visited.Contains(node)) return;

        Console.Write(node + " ");
        visited.Add(node);
        if (adjacencyList.ContainsKey(node))
        {
            foreach (var neighbor in adjacencyList[node])
            {
                DFSRecursive(neighbor, visited);
            }
        }
    }

    public void BreadthFirstSearch(int start)
    {
        HashSet<int> visited = new HashSet<int>();
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            int node = queue.Dequeue();
            Console.Write(node + " ");

            if (adjacencyList.ContainsKey(node))
            {
                foreach (var neighbor in adjacencyList[node])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }
        Console.WriteLine();
    }
}