using LoadBalancingAlgorithmsDemo.Presentation;

namespace LoadBalancingAlgorithmsDemo.Scenarios;

public interface ILoadBalancingScenario
{
    void Run(ConsoleWriter writer);
}