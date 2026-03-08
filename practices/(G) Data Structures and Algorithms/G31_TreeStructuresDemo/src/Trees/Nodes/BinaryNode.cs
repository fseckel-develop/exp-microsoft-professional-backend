using TreeStructuresDemo.Models;

namespace TreeStructuresDemo.Trees.Nodes;

public sealed class BinaryNode
{
    public ContentItem Value { get; set; }
    public BinaryNode? Left { get; set; }
    public BinaryNode? Right { get; set; }
    public int Height { get; set; } = 1;

    public BinaryNode(ContentItem value)
    {
        Value = value;
    }
}