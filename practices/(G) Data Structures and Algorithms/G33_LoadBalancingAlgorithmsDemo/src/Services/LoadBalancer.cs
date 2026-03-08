using LoadBalancingAlgorithmsDemo.Algorithms;
using LoadBalancingAlgorithmsDemo.Models;

namespace LoadBalancingAlgorithmsDemo.Services;

public sealed class LoadBalancer
{
    private readonly List<BackendInstance> _backends = [];

    public void RegisterBackend(BackendInstance backend)
    {
        _backends.Add(backend);
    }

    public IReadOnlyList<BackendInstance> Backends => _backends;

    public RoutingResult RouteRequest(ILoadBalancingStrategy strategy, ClientRequest request)
    {
        var backend = strategy.SelectBackend(_backends, request);
        backend.AcceptRequest();

        return new RoutingResult(
            Strategy: strategy.Name,
            ClientIp: request.ClientIp,
            Path: request.Path,
            BackendId: backend.Id);
    }

    public void CompleteAllConnections()
    {
        foreach (var backend in _backends)
        {
            while (backend.ActiveConnections > 0)
                backend.CompleteRequest();
        }
    }

    public IReadOnlyList<BackendInstance> SnapshotBackends()
    {
        return _backends.Select(x => x.Snapshot()).ToArray();
    }
}