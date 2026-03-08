using TreeStructuresDemo.Models;
using TreeStructuresDemo.Trees.Nodes;

namespace TreeStructuresDemo.Trees;

public sealed class BinarySearchTree : BinaryTree
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

        UpdateHeight(node);
        return node;
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
}