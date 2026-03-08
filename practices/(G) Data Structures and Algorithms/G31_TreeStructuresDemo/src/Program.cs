using TreeStructuresDemo.Presentation;
using TreeStructuresDemo.Scenarios;
using TreeStructuresDemo.Models;

namespace ContentTreeStructuresDemo;

internal static class Program
{
    private static void Main()
    {
        var dataset = DemoDatasetFactory.CreateBaseDataset();
        var writer = new ConsoleWriter();

        var scenarios = new ITreeDemoScenario[]
        {
            new GeneralTreeScenario(dataset),
            new BinaryTreeScenario(dataset),
            new BinarySearchTreeScenario(dataset),
            new AvlTreeScenario(dataset),
            new LookupComparisonDemo(dataset)
        };

        writer.WriteTitle(dataset.Title);
        writer.WriteSection("Scenario", dataset.Description);

        foreach (var scenario in scenarios)
        {
            scenario.Run(writer);
            writer.WriteSpacer();
        }
    }
}