public class BinaryTree
{
    public BinaryNode? Root { get; protected set; }

    public virtual void Insert(int data)
    {
        BinaryNode newNode = new BinaryNode(data);

        if (Root == null)
        {
            Root = newNode;
            return;
        }

        Queue<BinaryNode> queue = new Queue<BinaryNode>();
        queue.Enqueue(Root);

        while (queue.Count > 0)
        {
            BinaryNode current = queue.Dequeue();

            if (current.Left == null)
            {
                current.Left = newNode;
                break;
            }
            else
            {
                queue.Enqueue(current.Left);
            }

            if (current.Right == null)
            {
                current.Right = newNode;
                break;
            }
            else
            {
                queue.Enqueue(current.Right);
            }
        }

        CalculateHeight(Root);
    }

    protected int GetHeight(BinaryNode? node)
        => node?.Height ?? 0;

    protected void UpdateHeight(BinaryNode node)
        => node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
    
    private int CalculateHeight(BinaryNode? node)
    {
        if (node == null) return 0;

        node.Height = 1 + Math.Max(
            CalculateHeight(node.Left),
            CalculateHeight(node.Right)
        );

        return node.Height;
    }

    public void PreOrderTraversal(BinaryNode? node)
    {
        if (node != null)
        {
            Console.Write(node.Data + " ");
            PreOrderTraversal(node.Left);
            PreOrderTraversal(node.Right);
        }
    }

    public void InOrderTraversal(BinaryNode? node)
    {
        if (node != null)
        {
            InOrderTraversal(node.Left);
            Console.Write(node.Data + " ");
            InOrderTraversal(node.Right);
        }
    }

    public void PostOrderTraversal(BinaryNode? node)
    {
        if (node != null)
        {
            PostOrderTraversal(node.Left);
            PostOrderTraversal(node.Right);
            Console.Write(node.Data + " ");
        }
    }

    public void LevelOrderTraversal(BinaryNode? node)
    {
        if (node == null) return;

        Queue<BinaryNode> queue = new Queue<BinaryNode>();
        queue.Enqueue(node);

        while (queue.Count > 0)
        {
            BinaryNode current = queue.Dequeue();
            Console.Write(current.Data + " ");

            if (current.Left != null)
            {
                queue.Enqueue(current.Left);
            }
            if (current.Right != null)
            {
                queue.Enqueue(current.Right);
            }
        }
    }
}