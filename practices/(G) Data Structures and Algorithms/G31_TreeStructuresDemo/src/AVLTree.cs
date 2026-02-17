public class AVLTree : BinaryTree
{
    public override void Insert(int data)
    {
        Root = InsertRecursively(Root, data);
    }

    private BinaryNode InsertRecursively(BinaryNode? node, int data)
    {
        if (node == null)
        {
            return new BinaryNode(data);
        }
            
        if (data < node.Data) 
        {
            node.Left = InsertRecursively(node.Left, data);
        }
        else if (data > node.Data)
        {
            node.Right = InsertRecursively(node.Right, data);
        }
        else
        {
            return node;
        }
            
        UpdateHeight(node);

        return Balance(node);
    }

    private int GetBalance(BinaryNode node)
        => GetHeight(node.Left) - GetHeight(node.Right);

    private BinaryNode Balance(BinaryNode node)
    {
        int balance = GetBalance(node);

        // Left heavy
        if (balance > 1)
        {
            if (GetBalance(node.Left!) < 0)
            {
                node.Left = RotateLeft(node.Left!); // LR case
            }
                
            return RotateRight(node); // LL case
        }

        // Right heavy
        if (balance < -1)
        {
            if (GetBalance(node.Right!) > 0)
            {
                node.Right = RotateRight(node.Right!); // RL case
            }

            return RotateLeft(node); // RR case
        }

        return node;
    }

    private BinaryNode RotateRight(BinaryNode y)
    {
        BinaryNode x = y.Left!;
        BinaryNode? t2 = x.Right;

        x.Right = y;
        y.Left = t2;

        UpdateHeight(y);
        UpdateHeight(x);

        return x;
    }

    private BinaryNode RotateLeft(BinaryNode x)
    {
        BinaryNode y = x.Right!;
        BinaryNode? t2 = y.Left;

        y.Left = x;
        x.Right = t2;

        UpdateHeight(x);
        UpdateHeight(y);

        return y;
    }
}