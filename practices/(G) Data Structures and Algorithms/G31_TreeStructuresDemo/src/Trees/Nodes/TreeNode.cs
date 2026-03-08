using TreeStructuresDemo.Models;

namespace TreeStructuresDemo.Trees.Nodes;

public sealed class TreeNode
{
    public ContentCategory Value { get; set; }
    public List<TreeNode> Children { get; } = [];

    public TreeNode(ContentCategory value)
    {
        Value = value;
    }
}