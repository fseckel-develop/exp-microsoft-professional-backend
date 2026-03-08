using GraphCampusNavigator.Models;
using GraphCampusNavigator.Graphs;

namespace GraphCampusNavigator.Algorithms;

public sealed class AStarPathfinder
{
    public PathResult FindPath(WeightedGraph graph, string start, string goal)
    {
        var openSet = new PriorityQueue<string, int>();
        var cameFrom = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);

        var gScore = graph.Nodes.ToDictionary(n => n, _ => int.MaxValue, StringComparer.OrdinalIgnoreCase);
        var fScore = graph.Nodes.ToDictionary(n => n, _ => int.MaxValue, StringComparer.OrdinalIgnoreCase);

        gScore[start] = 0;
        fScore[start] = Heuristic(graph, start, goal);

        openSet.Enqueue(start, fScore[start]);
        cameFrom[start] = null;

        while (openSet.Count > 0)
        {
            openSet.TryDequeue(out var current, out _);

            if (current is null)
                continue;

            if (string.Equals(current, goal, StringComparison.OrdinalIgnoreCase))
            {
                var path = ReconstructPath(cameFrom, current);
                return new PathResult(path, gScore[current], true);
            }

            foreach (var edge in graph.GetWeightedNeighbors(current))
            {
                int tentativeGScore = gScore[current] + edge.Weight;

                if (tentativeGScore < gScore[edge.To])
                {
                    cameFrom[edge.To] = current;
                    gScore[edge.To] = tentativeGScore;
                    fScore[edge.To] = tentativeGScore + Heuristic(graph, edge.To, goal);

                    openSet.Enqueue(edge.To, fScore[edge.To]);
                }
            }
        }

        return new PathResult([], 0, false);
    }

    private static int Heuristic(WeightedGraph graph, string from, string to)
    {
        var a = graph.Places[from];
        var b = graph.Places[to];

        int dx = Math.Abs(a.X - b.X);
        int dy = Math.Abs(a.Y - b.Y);

        return dx + dy;
    }

    private static IReadOnlyList<string> ReconstructPath(
        Dictionary<string, string?> cameFrom,
        string goal)
    {
        var path = new List<string>();
        string? current = goal;

        while (current is not null)
        {
            path.Add(current);
            cameFrom.TryGetValue(current, out current);
        }

        path.Reverse();
        return path;
    }
}