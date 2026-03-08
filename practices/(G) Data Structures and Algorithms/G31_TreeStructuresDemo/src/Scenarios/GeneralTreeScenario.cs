using TreeStructuresDemo.Presentation;
using TreeStructuresDemo.Models;
using TreeStructuresDemo.Trees;

namespace TreeStructuresDemo.Scenarios;

public sealed class GeneralTreeScenario : ITreeDemoScenario
{
    private readonly DemoDataset _scenario;

    public GeneralTreeScenario(DemoDataset scenario)
    {
        _scenario = scenario;
    }

    public void Run(ConsoleWriter writer)
    {
        var tree = new GeneralTree(_scenario.RootCategory);

        var root = tree.Root!;
        var account = tree.AddChild(root, new ContentCategory(2, "Account"));
        tree.AddChild(root, new ContentCategory(3, "Billing"));
        var technical = tree.AddChild(root, new ContentCategory(4, "Technical"));

        tree.AddChild(account, new ContentCategory(5, "Password Reset"));
        tree.AddChild(account, new ContentCategory(6, "Profile Settings"));
        tree.AddChild(technical, new ContentCategory(7, "App Crashes"));
        tree.AddChild(technical, new ContentCategory(8, "Connectivity"));

        writer.WriteSection(
            "General Tree",
            "Represents the category hierarchy of a knowledge base.");

        writer.WriteTraversal("PreOrder", TraversalFormatter.FormatCategories(tree.PreOrder(tree.Root)));
        writer.WriteTraversal("PostOrder", TraversalFormatter.FormatCategories(tree.PostOrder(tree.Root)));
        writer.WriteTraversal("LevelOrder", TraversalFormatter.FormatCategories(tree.LevelOrder(tree.Root)));
    }
}