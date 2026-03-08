namespace LoadBalancingAlgorithmsDemo.Models;

public sealed class DemoDataset
{
    public required string Title { get; init; }
    public required string Description { get; init; }

    public required BackendInstance[] Backends { get; init; }
    public required ClientRequest[] Requests { get; init; }
}