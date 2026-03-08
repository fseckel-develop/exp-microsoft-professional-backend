using TreeStructuresDemo.Presentation;
using TreeStructuresDemo.Models;
using TreeStructuresDemo.Trees;

namespace TreeStructuresDemo.Scenarios;

public sealed class AvlTreeScenario : ITreeDemoScenario
{
    private readonly DemoDataset _scenario;

    public AvlTreeScenario(DemoDataset scenario)
    {
        _scenario = scenario;
    }

    public void Run(ConsoleWriter writer)
    {
        var tree = new AvlTree();

        foreach (var item in _scenario.SearchItems)
            tree.Insert(item);

        writer.WriteSection(
            "AVL Tree",
            "Represents a self-balancing content index for more stable lookup depth.");

        writer.WriteTraversal("InOrder", TraversalFormatter.FormatItems(tree.InOrder(tree.Root)));
        writer.WriteTraversal("PreOrder", TraversalFormatter.FormatItems(tree.PreOrder(tree.Root)));
        writer.WriteTraversal("PostOrder", TraversalFormatter.FormatItems(tree.PostOrder(tree.Root)));
        writer.WriteTraversal("LevelOrder", TraversalFormatter.FormatItems(tree.LevelOrder(tree.Root)));
        writer.WriteMetric("Height", tree.Root?.Height);
    }
}