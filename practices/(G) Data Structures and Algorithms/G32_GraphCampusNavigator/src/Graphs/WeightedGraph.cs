using GraphCampusNavigator.Models;

namespace GraphCampusNavigator.Graphs;

public sealed class WeightedGraph : IGraph<string>
{
    private readonly Dictionary<string, List<Edge>> _adjacency = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, Place> _places = new(StringComparer.OrdinalIgnoreCase);

    public void AddPlace(Place place)
    {
        _places[place.Name] = place;

        if (!_adjacency.ContainsKey(place.Name))
            _adjacency[place.Name] = [];
    }

    public void AddEdge(string from, string to, int weight, bool directed = false)
    {
        if (!_adjacency.ContainsKey(from))
            _adjacency[from] = [];

        if (!_adjacency.ContainsKey(to))
            _adjacency[to] = [];

        _adjacency[from].Add(new Edge(to, weight));

        if (!directed)
            _adjacency[to].Add(new Edge(from, weight));
    }

    public IEnumerable<string> GetNeighbors(string node)
    {
        return _adjacency.TryGetValue(node, out var neighbors)
            ? neighbors.Select(edge => edge.To)
            : [];
    }

    public IEnumerable<Edge> GetWeightedNeighbors(string node)
    {
        return _adjacency.TryGetValue(node, out var neighbors)
            ? neighbors
            : [];
    }

    public IReadOnlyDictionary<string, List<Edge>> Adjacency => _adjacency;

    public IReadOnlyDictionary<string, Place> Places => _places;

    public IEnumerable<string> Nodes => _adjacency.Keys;
}