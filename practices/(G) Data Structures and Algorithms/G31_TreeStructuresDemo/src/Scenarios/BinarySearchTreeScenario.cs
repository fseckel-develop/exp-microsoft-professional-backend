using TreeStructuresDemo.Presentation;
using TreeStructuresDemo.Models;
using TreeStructuresDemo.Trees;

namespace TreeStructuresDemo.Scenarios;

public sealed class BinarySearchTreeScenario : ITreeDemoScenario
{
    private readonly DemoDataset _scenario;

    public BinarySearchTreeScenario(DemoDataset scenario)
    {
        _scenario = scenario;
    }

    public void Run(ConsoleWriter writer)
    {
        var tree = new BinarySearchTree();

        foreach (var item in _scenario.SearchItems)
            tree.Insert(item);

        writer.WriteSection(
            "Binary Search Tree",
            "Represents a searchable content index ordered by content id.");

        writer.WriteTraversal("InOrder", TraversalFormatter.FormatItems(tree.InOrder(tree.Root)));
        writer.WriteTraversal("PreOrder", TraversalFormatter.FormatItems(tree.PreOrder(tree.Root)));
        writer.WriteTraversal("PostOrder", TraversalFormatter.FormatItems(tree.PostOrder(tree.Root)));
        writer.WriteTraversal("LevelOrder", TraversalFormatter.FormatItems(tree.LevelOrder(tree.Root)));
        writer.WriteMetric("Height", tree.Root?.Height);
    }
}