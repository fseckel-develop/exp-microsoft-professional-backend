namespace GraphCampusNavigator.Graphs;

public sealed class UnweightedGraph : IGraph<string>
{
    private readonly Dictionary<string, List<string>> _adjacency = new(StringComparer.OrdinalIgnoreCase);

    public void AddEdge(string from, string to, bool directed = false)
    {
        if (!_adjacency.ContainsKey(from))
            _adjacency[from] = [];

        if (!_adjacency.ContainsKey(to))
            _adjacency[to] = [];

        _adjacency[from].Add(to);

        if (!directed)
            _adjacency[to].Add(from);
    }

    public IReadOnlyDictionary<string, List<string>> Adjacency => _adjacency;

    public IEnumerable<string> GetNeighbors(string node)
    {
        return _adjacency.TryGetValue(node, out var neighbors)
            ? neighbors
            : [];
    }

    public IEnumerable<string> Nodes => _adjacency.Keys;
}