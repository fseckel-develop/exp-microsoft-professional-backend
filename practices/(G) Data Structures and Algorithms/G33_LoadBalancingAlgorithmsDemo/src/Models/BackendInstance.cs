namespace LoadBalancingAlgorithmsDemo.Models;

public sealed class BackendInstance
{
    public string Id { get; }
    public int Weight { get; }
    public int ActiveConnections { get; private set; }
    public int TotalRequestsHandled { get; private set; }

    public BackendInstance(string id, int weight = 1)
    {
        if (weight <= 0)
            throw new ArgumentOutOfRangeException(nameof(weight), "Weight must be greater than zero.");

        Id = id;
        Weight = weight;
    }

    public void AcceptRequest()
    {
        ActiveConnections++;
        TotalRequestsHandled++;
    }

    public void CompleteRequest()
    {
        if (ActiveConnections > 0)
            ActiveConnections--;
    }

    public BackendInstance Snapshot()
    {
        return new BackendInstance(Id, Weight)
        {
            ActiveConnectionsInternal = ActiveConnections,
            TotalRequestsHandledInternal = TotalRequestsHandled
        };
    }

    private int ActiveConnectionsInternal
    {
        init => ActiveConnections = value;
    }

    private int TotalRequestsHandledInternal
    {
        init => TotalRequestsHandled = value;
    }
}