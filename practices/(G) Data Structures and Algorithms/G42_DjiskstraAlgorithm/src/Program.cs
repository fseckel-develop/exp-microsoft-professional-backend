class Program
{
    static void Main()
    {
        var graph = new WeightedGraph();
        graph.AddEdge("A", "B", 4);
        graph.AddEdge("A", "C", 2);
        graph.AddEdge("B", "C", 1);
        graph.AddEdge("B", "D", 5);
        graph.AddEdge("C", "D", 8);
        graph.AddEdge("C", "E", 10);
        graph.AddEdge("D", "E", 2);
        graph.AddEdge("D", "F", 6);
        graph.AddEdge("E", "G", 3);
        graph.AddEdge("F", "G", 1);
        graph.AddEdge("F", "H", 7);
        graph.AddEdge("G", "I", 4);
        graph.AddEdge("H", "I", 2);
        graph.AddEdge("H", "J", 5);
        graph.AddEdge("I", "J", 1);

        Console.WriteLine("Running Dijkstra’s Algorithm (Greedy)");
        Console.WriteLine("Starting from Node A\n");

        var shortestDistances = graph.Dijkstra("A");

        Console.WriteLine("Nodes sorted by distance from A:");
        foreach (var (node, distance) in shortestDistances.OrderBy(kvp => kvp.Value))
        {
            if (distance == int.MaxValue)
            {
                Console.WriteLine($"Node {node}: Unreachable");
            }
            else
            {
                Console.WriteLine($"Node {node}: {distance}");
            }
        }
    }
}