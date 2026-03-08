using LoadBalancingAlgorithmsDemo.Models;

namespace LoadBalancingAlgorithmsDemo.Algorithms;

public sealed class IpHashStrategy : ILoadBalancingStrategy
{
    public string Name => "IP Hashing";

    public BackendInstance SelectBackend(IReadOnlyList<BackendInstance> backends, ClientRequest request)
    {
        if (backends.Count == 0)
            throw new InvalidOperationException("No backend instances registered.");

        int index = Math.Abs(StringComparer.OrdinalIgnoreCase.GetHashCode(request.ClientIp)) % backends.Count;
        return backends[index];
    }
}