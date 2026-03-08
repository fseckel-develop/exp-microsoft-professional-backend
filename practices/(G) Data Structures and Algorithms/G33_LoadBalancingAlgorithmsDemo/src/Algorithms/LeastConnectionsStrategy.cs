using LoadBalancingAlgorithmsDemo.Models;

namespace LoadBalancingAlgorithmsDemo.Algorithms;

public sealed class LeastConnectionsStrategy : ILoadBalancingStrategy
{
    public string Name => "Least Connections";

    public BackendInstance SelectBackend(IReadOnlyList<BackendInstance> backends, ClientRequest request)
    {
        if (backends.Count == 0)
            throw new InvalidOperationException("No backend instances registered.");

        return backends
            .OrderBy(x => x.ActiveConnections)
            .ThenBy(x => x.Id, StringComparer.OrdinalIgnoreCase)
            .First();
    }
}