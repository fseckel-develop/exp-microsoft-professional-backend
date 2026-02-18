public class WeightedGraph
{

    private Dictionary<string, Dictionary<string, int>> adjacencyList;

    public WeightedGraph()
    {
        adjacencyList = new Dictionary<string, Dictionary<string, int>>();
    }

    public void AddEdge(string from, string to, int weight)
    {
        if (!adjacencyList.ContainsKey(from))
            adjacencyList[from] = new Dictionary<string, int>();

        if (!adjacencyList.ContainsKey(to))
            adjacencyList[to] = new Dictionary<string, int>();

        adjacencyList[from][to] = weight;
        adjacencyList[to][from] = weight;
    }

    public Dictionary<string, int> Dijkstra(string start)
    {
        var distances = adjacencyList.Keys.ToDictionary(node => node, node => int.MaxValue);
        distances[start] = 0;
        var priorityQueue = new SortedSet<(int distance, string node)> { (0, start) };
        var visited = new HashSet<string>();

        while (priorityQueue.Count > 0)
        {
            var (currentDistance, currentNode) = priorityQueue.First();
            priorityQueue.Remove(priorityQueue.First());

            if (visited.Contains(currentNode))
                continue;

            visited.Add(currentNode);

            foreach (var (neighbor, neighborDistance) in adjacencyList[currentNode])
            {
                int newDistance = currentDistance + neighborDistance;
                if (newDistance < distances[neighbor])
                {
                    priorityQueue.Remove((distances[neighbor], neighbor));
                    distances[neighbor] = newDistance;
                    priorityQueue.Add((newDistance, neighbor));
                }
            }
        }

        return distances;
    }
}
