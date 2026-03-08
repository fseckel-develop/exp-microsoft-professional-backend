using GraphCampusNavigator.Models;
using GraphCampusNavigator.Graphs;

namespace GraphCampusNavigator.Algorithms;

public sealed class DijkstraPathfinder
{
    public Dictionary<string, int> GetShortestDistances(WeightedGraph graph, string start)
    {
        var distances = graph.Nodes.ToDictionary(n => n, _ => int.MaxValue, StringComparer.OrdinalIgnoreCase);
        var queue = new PriorityQueue<string, int>();

        distances[start] = 0;
        queue.Enqueue(start, 0);

        while (queue.Count > 0)
        {
            queue.TryDequeue(out var current, out var currentDistance);

            if (current is null)
                continue;

            if (currentDistance > distances[current])
                continue;

            foreach (var edge in graph.GetWeightedNeighbors(current))
            {
                int newDistance = distances[current] + edge.Weight;

                if (newDistance < distances[edge.To])
                {
                    distances[edge.To] = newDistance;
                    queue.Enqueue(edge.To, newDistance);
                }
            }
        }

        return distances;
    }

    public PathResult FindShortestPath(WeightedGraph graph, string start, string goal)
    {
        var distances = graph.Nodes.ToDictionary(n => n, _ => int.MaxValue, StringComparer.OrdinalIgnoreCase);
        var previous = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
        var queue = new PriorityQueue<string, int>();

        distances[start] = 0;
        previous[start] = null;
        queue.Enqueue(start, 0);

        while (queue.Count > 0)
        {
            queue.TryDequeue(out var current, out var currentDistance);

            if (current is null)
                continue;

            if (currentDistance > distances[current])
                continue;

            if (string.Equals(current, goal, StringComparison.OrdinalIgnoreCase))
                break;

            foreach (var edge in graph.GetWeightedNeighbors(current))
            {
                int newDistance = distances[current] + edge.Weight;

                if (newDistance < distances[edge.To])
                {
                    distances[edge.To] = newDistance;
                    previous[edge.To] = current;
                    queue.Enqueue(edge.To, newDistance);
                }
            }
        }

        if (!distances.ContainsKey(goal) || distances[goal] == int.MaxValue)
            return new PathResult([], 0, false);

        var path = ReconstructPath(previous, goal);
        return new PathResult(path, distances[goal], true);
    }

    private static IReadOnlyList<string> ReconstructPath(
        Dictionary<string, string?> previous,
        string goal)
    {
        var path = new List<string>();
        string? current = goal;

        while (current is not null)
        {
            path.Add(current);
            previous.TryGetValue(current, out current);
        }

        path.Reverse();
        return path;
    }
}