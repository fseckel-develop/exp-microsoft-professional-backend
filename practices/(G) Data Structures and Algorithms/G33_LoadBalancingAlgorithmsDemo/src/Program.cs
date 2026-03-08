using LoadBalancingAlgorithmsDemo.Presentation;
using LoadBalancingAlgorithmsDemo.Scenarios;

namespace LoadBalancingAlgorithmsDemo;

internal static class Program
{
    private static void Main()
    {
        var dataset = DemoDatasetFactory.CreateGatewayRoutingDataset();
        var writer = new ConsoleWriter();

        var scenarios = new ILoadBalancingScenario[]
        {
            new StrategyComparisonScenario(dataset)
        };

        foreach (var scenario in scenarios)
        {
            scenario.Run(writer);
        }
    }
}