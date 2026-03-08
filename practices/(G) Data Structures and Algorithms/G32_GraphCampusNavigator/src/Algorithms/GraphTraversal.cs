using GraphCampusNavigator.Graphs;

namespace GraphCampusNavigator.Algorithms;

public static class GraphTraversal
{
    public static IReadOnlyList<TNode> BreadthFirst<TNode>(IGraph<TNode> graph, TNode start)
    {
        var visited = new HashSet<TNode>();
        var result = new List<TNode>();
        var queue = new Queue<TNode>();

        visited.Add(start);
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            result.Add(node);

            foreach (var neighbor in graph.GetNeighbors(node))
            {
                if (visited.Add(neighbor))
                    queue.Enqueue(neighbor);
            }
        }

        return result;
    }

    public static IReadOnlyList<TNode> DepthFirstIterative<TNode>(IGraph<TNode> graph, TNode start)
    {
        var visited = new HashSet<TNode>();
        var result = new List<TNode>();
        var stack = new Stack<TNode>();

        stack.Push(start);

        while (stack.Count > 0)
        {
            var node = stack.Pop();

            if (!visited.Add(node))
                continue;

            result.Add(node);

            foreach (var neighbor in graph.GetNeighbors(node).Reverse())
                stack.Push(neighbor);
        }

        return result;
    }

    public static IReadOnlyList<TNode> DepthFirstRecursive<TNode>(IGraph<TNode> graph, TNode start)
    {
        var visited = new HashSet<TNode>();
        var result = new List<TNode>();

        Traverse(graph, start, visited, result);

        return result;
    }

    private static void Traverse<TNode>(
        IGraph<TNode> graph,
        TNode node,
        HashSet<TNode> visited,
        List<TNode> result)
    {
        if (!visited.Add(node))
            return;

        result.Add(node);

        foreach (var neighbor in graph.GetNeighbors(node))
            Traverse(graph, neighbor, visited, result);
    }
}