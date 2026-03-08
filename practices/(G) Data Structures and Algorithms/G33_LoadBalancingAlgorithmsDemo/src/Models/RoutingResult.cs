namespace LoadBalancingAlgorithmsDemo.Models;

public sealed record RoutingResult(
    string Strategy,
    string ClientIp,
    string Path,
    string BackendId
);