using LoadBalancingAlgorithmsDemo.Models;

namespace LoadBalancingAlgorithmsDemo.Algorithms;

public sealed class RoundRobinStrategy : ILoadBalancingStrategy
{
    private int _lastIndex = -1;

    public string Name => "Round Robin";

    public BackendInstance SelectBackend(IReadOnlyList<BackendInstance> backends, ClientRequest request)
    {
        EnsureBackends(backends);

        _lastIndex = (_lastIndex + 1) % backends.Count;
        return backends[_lastIndex];
    }

    private static void EnsureBackends(IReadOnlyList<BackendInstance> backends)
    {
        if (backends.Count == 0)
            throw new InvalidOperationException("No backend instances registered.");
    }
}