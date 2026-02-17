public class BinarySearchTree : BinaryTree
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

        UpdateHeight(node);

        return node;
    }
}
