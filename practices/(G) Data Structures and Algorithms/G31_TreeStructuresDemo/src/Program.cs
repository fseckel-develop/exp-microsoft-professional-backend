using System;
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("===== GENERAL TREE =====");
        TestGeneralTree();

        Console.WriteLine("\n===== BINARY TREE (Level Insert) =====");
        TestBinaryTree();

        Console.WriteLine("\n===== BINARY SEARCH TREE =====");
        TestBinarySearchTree();

        Console.WriteLine("\n===== AVL TREE =====");
        TestAVLTree();

        Console.WriteLine("\nDone.");
    }

    private static void TestGeneralTree()
    {
        var tree = new GeneralTree(1);

        var root = tree.Root!;
        tree.AddChild(root, 2);
        tree.AddChild(root, 3);
        tree.AddChild(root, 4);

        tree.AddChild(root.Children[0], 5);
        tree.AddChild(root.Children[0], 6);

        Console.Write("PreOrder (DFS): ");
        tree.PreOrderTraversal(tree.Root);

        Console.Write("\nPostOrder: ");
        tree.PostOrderTraversal(tree.Root);

        Console.Write("\nLevelOrder (BFS): ");
        tree.LevelOrderTraversal(tree.Root);

        Console.WriteLine();
    }

    private static void TestBinaryTree()
    {
        var tree = new BinaryTree();

        int[] values = { 10, 20, 30, 40, 50, 60 };
        foreach (var value in values)
        {
            tree.Insert(value);
        }

        Console.Write("PreOrder: ");
        tree.PreOrderTraversal(tree.Root);

        Console.Write("\nInOrder: ");
        tree.InOrderTraversal(tree.Root);

        Console.Write("\nPostOrder: ");
        tree.PostOrderTraversal(tree.Root);

        Console.Write("\nLevelOrder: ");
        tree.LevelOrderTraversal(tree.Root);

        Console.WriteLine($"\nHeight: {tree.Root?.Height}");
    }

    private static void TestBinarySearchTree()
    {
        var bst = new BinarySearchTree();

        int[] values = { 10, 20, 30, 40, 50, 25 };
        foreach (var value in values)
        {
            bst.Insert(value);
        }

        Console.Write("InOrder (Sorted): ");
        bst.InOrderTraversal(bst.Root);

        Console.Write("\nPreOrder: ");
        bst.PreOrderTraversal(bst.Root);

        Console.Write("\nPostOrder: ");
        bst.PostOrderTraversal(bst.Root);

        Console.Write("\nLevelOrder: ");
        bst.LevelOrderTraversal(bst.Root);

        Console.WriteLine($"\nHeight: {bst.Root?.Height}");
    }

    private static void TestAVLTree()
    {
        var avl = new AVLTree();

        // Intentionally unbalanced input
        int[] values = { 10, 20, 30, 40, 50, 25 };
        foreach (var value in values)
        {
            avl.Insert(value);
        }

        Console.Write("InOrder (Sorted): ");
        avl.InOrderTraversal(avl.Root);

        Console.Write("\nPreOrder (Shows balancing effect): ");
        avl.PreOrderTraversal(avl.Root);

        Console.Write("\nPostOrder: ");
        avl.PostOrderTraversal(avl.Root);

        Console.Write("\nLevelOrder: ");
        avl.LevelOrderTraversal(avl.Root);

        Console.WriteLine($"\nHeight (Balanced): {avl.Root?.Height}");
    }
}