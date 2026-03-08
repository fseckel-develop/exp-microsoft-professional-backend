using GraphCampusNavigator.Graphs;

namespace GraphCampusNavigator.Models;

public sealed class DemoDataset
{
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required UnweightedGraph TraversalGraph { get; init; }
    public required WeightedGraph RouteGraph { get; init; }
}