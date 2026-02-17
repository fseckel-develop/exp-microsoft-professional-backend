public class TreeNode
{
    public int Data { get; set; }
    public List<TreeNode> Children { get; set; }

    public TreeNode(int data)
    {
        Data = data;
        Children = new List<TreeNode>();
    }
}

public class BinaryNode
{
    public int Data { get; set; }
    public BinaryNode? Left { get; set; }
    public BinaryNode? Right { get; set; }
    public int Height { get; internal set; }

    public BinaryNode(int data)
    {
        Data = data;
        Left = null;
        Right = null;
        Height = 1;
    }
}
