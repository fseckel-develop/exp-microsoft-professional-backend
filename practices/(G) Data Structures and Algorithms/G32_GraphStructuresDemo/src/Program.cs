public class Program
{
    public static void Main()
    {
        Graph graph = new Graph();

        // ----- Add 10 undirected edges -----
        graph.AddEdge(1, 2);
        graph.AddEdge(1, 3);
        graph.AddEdge(2, 4);
        graph.AddEdge(2, 5);
        graph.AddEdge(3, 6);
        graph.AddEdge(3, 7);
        graph.AddEdge(4, 8);
        graph.AddEdge(5, 9);
        graph.AddEdge(6, 10);
        graph.AddEdge(7, 10);

        // ----- Add 3 directed edges -----
        graph.AddEdge(8, 9, directed: true);
        graph.AddEdge(9, 10, directed: true);
        graph.AddEdge(5, 6, directed: true);

        Console.WriteLine("\n===== Adjacency List =====");
        graph.PrintAdjacencyList();

        Console.WriteLine("\n===== DFS Recursive from node 1 =====");
        graph.DepthFirstSearch(1, iterative: false);

        Console.WriteLine("\n===== DFS Iterative from node 1 =====");
        graph.DepthFirstSearch(1, iterative: true);

        Console.WriteLine("\n===== BFS from node 1 =====");
        graph.BreadthFirstSearch(1);
    }
}

