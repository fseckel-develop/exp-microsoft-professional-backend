public class Server
{
    public string Id { get; }
    public int RequestCount { get; private set; }
    public int Weight { get; }

    public Server(string id, int weight = 1)
    {
        Id = id;
        RequestCount = 0;
        Weight = weight;
    }

    public void HandleRequest()
    {
        RequestCount++;
    }
}