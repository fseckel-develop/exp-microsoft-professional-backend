using TreeStructuresDemo.Presentation;
using TreeStructuresDemo.Models;
using TreeStructuresDemo.Trees;

namespace TreeStructuresDemo.Scenarios;

public sealed class LookupComparisonDemo : ITreeDemoScenario
{
    private readonly DemoDataset _scenario;
    private readonly int _searchId;

    public LookupComparisonDemo(DemoDataset scenario, int searchId = 25)
    {
        _scenario = scenario;
        _searchId = searchId;
    }

    public void Run(ConsoleWriter writer)
    {
        var bst = new BinarySearchTree();
        var avl = new AvlTree();

        foreach (var item in _scenario.SearchItems)
        {
            bst.Insert(item);
            avl.Insert(item);
        }

        var bstResult = bst.FindById(_searchId);
        var avlResult = avl.FindById(_searchId);

        writer.WriteSection(
            "Lookup Comparison",
            "Compare how many steps BST and AVL need to find the same content item.");

        writer.WriteSearchComparison(_searchId, bstResult, avlResult);
        writer.WriteMetric("BST height", bst.Root?.Height);
        writer.WriteMetric("AVL height", avl.Root?.Height);
    }
}