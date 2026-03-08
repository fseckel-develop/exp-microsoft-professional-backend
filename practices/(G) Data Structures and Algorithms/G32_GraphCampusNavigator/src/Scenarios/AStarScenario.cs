using GraphCampusNavigator.Algorithms;
using GraphCampusNavigator.Models;
using GraphCampusNavigator.Presentation;

namespace GraphCampusNavigator.Scenarios;

public sealed class AStarScenario : IGraphDemoScenario
{
    private readonly DemoDataset _dataset;
    private readonly string _startNode;
    private readonly string _goalNode;

    public AStarScenario(
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
        var pathfinder = new AStarPathfinder();

        writer.WriteSection(
            "A* Pathfinding",
            "Find a route using weighted edges plus a coordinate-based heuristic.\n" +
            "Note: A* may return non-optimal paths if heuristic overestimates remaining cost.");

        var route = pathfinder.FindPath(_dataset.RouteGraph, _startNode, _goalNode);
        writer.WritePathResult($"Route from {_startNode} to {_goalNode} (A*)", route);
    }
}