using GraphCampusNavigator.Algorithms;
using GraphCampusNavigator.Models;
using GraphCampusNavigator.Presentation;

namespace GraphCampusNavigator.Scenarios;

public sealed class DijkstraScenario : IGraphDemoScenario
{
    private readonly DemoDataset _dataset;
    private readonly string _startNode;
    private readonly string _goalNode;

    public DijkstraScenario(
        DemoDataset dataset,
        string startNode = "Entrance",
        string goalNode = "Parking")
    {
        _dataset = dataset;
        _startNode = startNode;
        _goalNode = goalNode;
    }

    public void Run(ConsoleWriter writer)
    {
        var dijkstra = new DijkstraPathfinder();

        writer.WriteSection(
            "Dijkstra Shortest Paths",
            "Find the lowest-cost routes in the weighted campus graph.");

        writer.WriteWeightedAdjacencyList(_dataset.RouteGraph);

        var distances = dijkstra.GetShortestDistances(_dataset.RouteGraph, _startNode);
        writer.WriteDistances(_startNode, distances);

        var route = dijkstra.FindShortestPath(_dataset.RouteGraph, _startNode, _goalNode);
        writer.WritePathResult($"Shortest route from {_startNode} to {_goalNode} (Dijkstra)", route);
    }
}