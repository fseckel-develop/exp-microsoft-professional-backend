namespace LoadBalancingAlgorithmsDemo.Models;

public sealed record ClientRequest(
    string ClientIp,
    string Path
);