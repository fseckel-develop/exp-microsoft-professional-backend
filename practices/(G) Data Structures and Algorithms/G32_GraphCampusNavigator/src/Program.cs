using GraphCampusNavigator.Presentation;
using GraphCampusNavigator.Scenarios;

namespace GraphCampusNavigator;

internal static class Program
{
    private static void Main()
    {
        var dataset = DemoDatasetFactory.CreateCampusNavigationDataset();
        var writer = new ConsoleWriter();

        var scenarios = new IGraphDemoScenario[]
        {
            new TraversalScenario(dataset),
            new DijkstraScenario(dataset),
            new AStarScenario(dataset)
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