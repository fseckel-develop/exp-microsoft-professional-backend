using LoadBalancingAlgorithmsDemo.Models;

namespace LoadBalancingAlgorithmsDemo.Algorithms;

public sealed class WeightedRoundRobinStrategy : ILoadBalancingStrategy
{
    private int _currentIndex = -1;
    private int _currentWeightUsage = 0;

    public string Name => "Weighted Round Robin";

    public BackendInstance SelectBackend(IReadOnlyList<BackendInstance> backends, ClientRequest request)
    {
        if (backends.Count == 0)
            throw new InvalidOperationException("No backend instances registered.");

        while (true)
        {
            if (_currentIndex == -1 || _currentWeightUsage >= backends[_currentIndex].Weight)
            {
                _currentIndex = (_currentIndex + 1) % backends.Count;
                _currentWeightUsage = 0;
            }

            _currentWeightUsage++;
            return backends[_currentIndex];
        }
    }
}