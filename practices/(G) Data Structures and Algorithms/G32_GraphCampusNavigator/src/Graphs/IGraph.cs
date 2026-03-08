namespace GraphCampusNavigator.Graphs;

public interface IGraph<TNode>
{
    IEnumerable<TNode> Nodes { get; }
    IEnumerable<TNode> GetNeighbors(TNode node);
}