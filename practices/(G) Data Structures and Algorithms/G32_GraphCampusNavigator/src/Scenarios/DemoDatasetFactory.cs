using GraphCampusNavigator.Models;
using GraphCampusNavigator.Graphs;

namespace GraphCampusNavigator.Scenarios;

public static class DemoDatasetFactory
{
    public static DemoDataset CreateCampusNavigationDataset()
    {
        return new DemoDataset
        {
            Title = "Campus Navigation Graph Demo",
            Description = "Demonstrates graph traversal and shortest-path algorithms in a campus navigation domain.",
            TraversalGraph = CreateTraversalGraph(),
            RouteGraph = CreateRouteGraph()
        };
    }

    private static UnweightedGraph CreateTraversalGraph()
    {
        var graph = new UnweightedGraph();

        graph.AddEdge("Entrance", "Library");
        graph.AddEdge("Entrance", "Cafeteria");
        graph.AddEdge("Library", "Lab");
        graph.AddEdge("Library", "Student Center");
        graph.AddEdge("Cafeteria", "Auditorium");
        graph.AddEdge("Cafeteria", "Gym");
        graph.AddEdge("Lab", "Research Wing");
        graph.AddEdge("Student Center", "Dormitory");
        graph.AddEdge("Auditorium", "Parking");
        graph.AddEdge("Gym", "Parking");

        graph.AddEdge("Research Wing", "Dormitory", directed: true);
        graph.AddEdge("Dormitory", "Parking", directed: true);
        graph.AddEdge("Student Center", "Auditorium", directed: true);

        return graph;
    }

    private static WeightedGraph CreateRouteGraph()
    {
        var graph = new WeightedGraph();

        graph.AddPlace(new("Entrance", 0, 0));
        graph.AddPlace(new("Library", 2, 1));
        graph.AddPlace(new("Cafeteria", 2, -1));
        graph.AddPlace(new("Lab", 4, 2));
        graph.AddPlace(new("Student Center", 4, 0));
        graph.AddPlace(new("Auditorium", 4, -2));
        graph.AddPlace(new("Dormitory", 6, 1));
        graph.AddPlace(new("Gym", 6, -1));
        graph.AddPlace(new("Parking", 8, 0));

        graph.AddEdge("Entrance", "Library", 3);
        graph.AddEdge("Entrance", "Cafeteria", 2);
        graph.AddEdge("Library", "Lab", 3);
        graph.AddEdge("Library", "Student Center", 2);
        graph.AddEdge("Cafeteria", "Student Center", 3);
        graph.AddEdge("Cafeteria", "Auditorium", 4);
        graph.AddEdge("Lab", "Dormitory", 4);
        graph.AddEdge("Student Center", "Dormitory", 3);
        graph.AddEdge("Student Center", "Gym", 4);
        graph.AddEdge("Auditorium", "Gym", 2);
        graph.AddEdge("Dormitory", "Parking", 3);
        graph.AddEdge("Gym", "Parking", 2);

        return graph;
    }
}