using TreeStructuresDemo.Models;
using TreeStructuresDemo.Trees.Nodes;

namespace TreeStructuresDemo.Trees;

public sealed class AvlTree : BinaryTree
{
    public override void Insert(ContentItem item)
    {
        Root = InsertRecursive(Root, item);
    }

    private BinaryNode InsertRecursive(BinaryNode? node, ContentItem item)
    {
        if (node is null)
            return new BinaryNode(item);

        if (item.Id < node.Value.Id)
            node.Left = InsertRecursive(node.Left, item);
        else if (item.Id > node.Value.Id)
            node.Right = InsertRecursive(node.Right, item);
        else
            return node;

        UpdateHeight(node);
        return Balance(node);
    }

    public SearchResult<ContentItem> FindById(int id)
    {
        int steps = 0;
        var current = Root;

        while (current is not null)
        {
            steps++;

            if (id == current.Value.Id)
                return new SearchResult<ContentItem>(current.Value, true, steps);

            current = id < current.Value.Id
                ? current.Left
                : current.Right;
        }

        return new SearchResult<ContentItem>(null, false, steps);
    }

    private int GetBalance(BinaryNode node) => GetHeight(node.Left) - GetHeight(node.Right);

    private BinaryNode Balance(BinaryNode node)
    {
        int balance = GetBalance(node);

        if (balance > 1)
        {
            if (GetBalance(node.Left!) < 0)
                node.Left = RotateLeft(node.Left!);

            return RotateRight(node);
        }

        if (balance < -1)
        {
            if (GetBalance(node.Right!) > 0)
                node.Right = RotateRight(node.Right!);

            return RotateLeft(node);
        }

        return node;
    }

    private BinaryNode RotateRight(BinaryNode pivot)
    {
        var newRoot = pivot.Left!;
        var transferredSubtree = newRoot.Right;

        newRoot.Right = pivot;
        pivot.Left = transferredSubtree;

        UpdateHeight(pivot);
        UpdateHeight(newRoot);

        return newRoot;
    }

    private BinaryNode RotateLeft(BinaryNode pivot)
    {
        var newRoot = pivot.Right!;
        var transferredSubtree = newRoot.Left;

        newRoot.Left = pivot;
        pivot.Right = transferredSubtree;

        UpdateHeight(pivot);
        UpdateHeight(newRoot);

        return newRoot;
    }
}