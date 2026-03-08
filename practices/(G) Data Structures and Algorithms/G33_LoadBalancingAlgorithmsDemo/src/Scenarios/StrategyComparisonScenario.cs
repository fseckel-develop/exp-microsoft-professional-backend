using LoadBalancingAlgorithmsDemo.Algorithms;
using LoadBalancingAlgorithmsDemo.Models;
using LoadBalancingAlgorithmsDemo.Services;
using LoadBalancingAlgorithmsDemo.Presentation;

namespace LoadBalancingAlgorithmsDemo.Scenarios;

public sealed class StrategyComparisonScenario : ILoadBalancingScenario
{
    private readonly DemoDataset _dataset;

    public StrategyComparisonScenario(DemoDataset dataset)
    {
        _dataset = dataset;
    }

    public void Run(ConsoleWriter writer)
    {
        writer.WriteTitle(_dataset.Title);
        writer.WriteSection("Scenario", _dataset.Description);

        var strategies = new ILoadBalancingStrategy[]
        {
            new RoundRobinStrategy(),
            new LeastConnectionsStrategy(),
            new IpHashStrategy(),
            new WeightedRoundRobinStrategy()
        };

        foreach (var strategy in strategies)
        {
            RunStrategy(strategy, writer);
            writer.WriteSpacer();
        }
    }

    private void RunStrategy(ILoadBalancingStrategy strategy, ConsoleWriter writer)
    {
        var loadBalancer = new LoadBalancer();

        foreach (var backend in _dataset.Backends)
        {
            loadBalancer.RegisterBackend(new BackendInstance(backend.Id, backend.Weight));
        }

        writer.WriteSection(
            strategy.Name,
            $"Routing requests using {strategy.Name} strategy.");

        foreach (var request in _dataset.Requests)
        {
            var result = loadBalancer.RouteRequest(strategy, request);
            writer.WriteRoutingResult(result);
        }

        writer.WriteBackendSummary(loadBalancer.SnapshotBackends());
    }
}