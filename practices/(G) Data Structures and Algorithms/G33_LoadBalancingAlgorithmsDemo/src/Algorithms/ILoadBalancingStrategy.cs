using LoadBalancingAlgorithmsDemo.Models;

namespace LoadBalancingAlgorithmsDemo.Algorithms;

public interface ILoadBalancingStrategy
{
    string Name { get; }
    BackendInstance SelectBackend(IReadOnlyList<BackendInstance> backends, ClientRequest request);
}