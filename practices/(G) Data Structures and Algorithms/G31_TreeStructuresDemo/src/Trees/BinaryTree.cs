using TreeStructuresDemo.Models;
using TreeStructuresDemo.Trees.Nodes;

namespace TreeStructuresDemo.Trees;

public class BinaryTree
{
    public BinaryNode? Root { get; protected set; }

    public virtual void Insert(ContentItem item)
    {
        var newNode = new BinaryNode(item);

        if (Root is null)
        {
            Root = newNode;
            return;
        }

        var queue = new Queue<BinaryNode>();
        queue.Enqueue(Root);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current.Left is null)
            {
                current.Left = newNode;
                break;
            }

            queue.Enqueue(current.Left);

            if (current.Right is null)
            {
                current.Right = newNode;
                break;
            }

            queue.Enqueue(current.Right);
        }

        RecalculateHeight(Root);
    }

    protected int GetHeight(BinaryNode? node) => node?.Height ?? 0;

    protected void UpdateHeight(BinaryNode node)
    {
        node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
    }

    private int RecalculateHeight(BinaryNode? node)
    {
        if (node is null)
            return 0;

        node.Height = 1 + Math.Max(
            RecalculateHeight(node.Left),
            RecalculateHeight(node.Right));

        return node.Height;
    }

    public IEnumerable<ContentItem> PreOrder(BinaryNode? node)
    {
        if (node is null)
            yield break;

        yield return node.Value;

        foreach (var item in PreOrder(node.Left))
            yield return item;

        foreach (var item in PreOrder(node.Right))
            yield return item;
    }

    public IEnumerable<ContentItem> InOrder(BinaryNode? node)
    {
        if (node is null)
            yield break;

        foreach (var item in InOrder(node.Left))
            yield return item;

        yield return node.Value;

        foreach (var item in InOrder(node.Right))
            yield return item;
    }

    public IEnumerable<ContentItem> PostOrder(BinaryNode? node)
    {
        if (node is null)
            yield break;

        foreach (var item in PostOrder(node.Left))
            yield return item;

        foreach (var item in PostOrder(node.Right))
            yield return item;

        yield return node.Value;
    }

    public IEnumerable<ContentItem> LevelOrder(BinaryNode? node)
    {
        if (node is null)
            yield break;

        var queue = new Queue<BinaryNode>();
        queue.Enqueue(node);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            yield return current.Value;

            if (current.Left is not null)
                queue.Enqueue(current.Left);

            if (current.Right is not null)
                queue.Enqueue(current.Right);
        }
    }
}