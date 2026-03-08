using TreeStructuresDemo.Models;
using TreeStructuresDemo.Trees.Nodes;

namespace TreeStructuresDemo.Trees;

public sealed class GeneralTree
{
    public TreeNode? Root { get; }

    public GeneralTree(ContentCategory rootCategory)
    {
        Root = new TreeNode(rootCategory);
    }

    public TreeNode AddChild(TreeNode parent, ContentCategory category)
    {
        var child = new TreeNode(category);
        parent.Children.Add(child);
        return child;
    }

    public IEnumerable<ContentCategory> PreOrder(TreeNode? node)
    {
        if (node is null)
            yield break;

        yield return node.Value;

        foreach (var child in node.Children)
        {
            foreach (var descendant in PreOrder(child))
                yield return descendant;
        }
    }

    public IEnumerable<ContentCategory> PostOrder(TreeNode? node)
    {
        if (node is null)
            yield break;

        foreach (var child in node.Children)
        {
            foreach (var descendant in PostOrder(child))
                yield return descendant;
        }

        yield return node.Value;
    }

    public IEnumerable<ContentCategory> LevelOrder(TreeNode? node)
    {
        if (node is null)
            yield break;

        var queue = new Queue<TreeNode>();
        queue.Enqueue(node);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            yield return current.Value;

            foreach (var child in current.Children)
                queue.Enqueue(child);
        }
    }
}