public class GeneralTree
{
    public TreeNode? Root { get; private set; }

    public GeneralTree(int rootData)
    {
        Root = new TreeNode(rootData);
    }

    public void AddChild(TreeNode parent, int data)
    {
        parent.Children.Add(new TreeNode(data));
    }

    public void PreOrderTraversal(TreeNode? node) // = Depth-First Traversal
    {
        if (node == null) return;

        Console.Write(node.Data + " ");

        foreach (var child in node.Children)
        {
            PreOrderTraversal(child);
        }
    }

    public void PostOrderTraversal(TreeNode? node)
    {
        if (node == null) return;

        foreach (var child in node.Children)
        {
            PostOrderTraversal(child);
        }

        Console.Write(node.Data + " ");
    }

    public void LevelOrderTraversal(TreeNode? node) // = Breadth-First Traversal
    {
        if (node == null) return;

        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(node);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            Console.Write(current.Data + " ");

            foreach (var child in current.Children)
            {
                queue.Enqueue(child);
            }
        }
    }
}