using TreeStructuresDemo.Presentation;
using TreeStructuresDemo.Models;
using TreeStructuresDemo.Trees;

namespace TreeStructuresDemo.Scenarios;

public sealed class BinaryTreeScenario : ITreeDemoScenario
{
    private readonly DemoDataset _scenario;

    public BinaryTreeScenario(DemoDataset scenario)
    {
        _scenario = scenario;
    }

    public void Run(ConsoleWriter writer)
    {
        var tree = new BinaryTree();

        foreach (var item in _scenario.ContentItems)
            tree.Insert(item);

        writer.WriteSection(
            "Binary Tree",
            "Represents level-order content placement without search ordering guarantees.");

        writer.WriteTraversal("InOrder", TraversalFormatter.FormatItems(tree.InOrder(tree.Root)));
        writer.WriteTraversal("PreOrder", TraversalFormatter.FormatItems(tree.PreOrder(tree.Root)));
        writer.WriteTraversal("PostOrder", TraversalFormatter.FormatItems(tree.PostOrder(tree.Root)));
        writer.WriteTraversal("LevelOrder", TraversalFormatter.FormatItems(tree.LevelOrder(tree.Root)));
        writer.WriteMetric("Height", tree.Root?.Height);
    }
}