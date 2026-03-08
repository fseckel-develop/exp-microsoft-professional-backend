using GraphCampusNavigator.Algorithms;
using GraphCampusNavigator.Models;
using GraphCampusNavigator.Presentation;

namespace GraphCampusNavigator.Scenarios;

public sealed class TraversalScenario : IGraphDemoScenario
{
    private readonly DemoDataset _dataset;
    private readonly string _startNode;

    public TraversalScenario(DemoDataset dataset, string startNode = "Entrance")
    {
        _dataset = dataset;
        _startNode = startNode;
    }

    public void Run(ConsoleWriter writer)
    {
        writer.WriteSection(
            "Graph Traversal",
            "Use DFS and BFS to explore reachable campus locations.");

        writer.WriteAdjacencyList(_dataset.TraversalGraph);

        var dfsRecursive = GraphTraversal.DepthFirstRecursive(_dataset.TraversalGraph, _startNode);
        var dfsIterative = GraphTraversal.DepthFirstIterative(_dataset.TraversalGraph, _startNode);
        var bfs = GraphTraversal.BreadthFirst(_dataset.TraversalGraph, _startNode);

        writer.WriteTraversal("DFS Recursive", dfsRecursive);
        writer.WriteTraversal("DFS Iterative", dfsIterative);
        writer.WriteTraversal("BFS", bfs);
    }
}