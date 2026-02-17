public class LoadBalancer
{
    private List<Server> servers = new List<Server>();
    private int lastUsedServer = -1;
    private int wrrCounter = 0; // Counter for Weighted Round Robin

    public void RegisterServer(Server server)
    {
        servers.Add(server);
    }

    public Server GetServerRoundRobin()
    {
        if (servers.Count == 0)
        {
            throw new InvalidOperationException("No servers registered");
        }

        lastUsedServer = (lastUsedServer + 1) % servers.Count;
        return servers[lastUsedServer];
    }

    public Server GetServerLeastConnections()
    {
        if (servers.Count == 0)
        {
            throw new InvalidOperationException("No servers registered");
        }

        return servers.OrderBy(s => s.RequestCount).First();
    }

    public Server GetServerIpHashing(string ipAddress)
    {
        if (servers.Count == 0)
        {
            throw new InvalidOperationException("No servers registered");
        }

        int index = Math.Abs(ipAddress.GetHashCode()) % servers.Count;
        return servers[index];
    }

    public Server GetServerWeightedRoundRobin()
    {
        if (servers.Count == 0)
        {
            throw new InvalidOperationException("No servers registered");
        }

        while (true)
        {
            lastUsedServer = (lastUsedServer + 1) % servers.Count;
            if (wrrCounter < servers[lastUsedServer].Weight)
            {
                wrrCounter++;
                return servers[lastUsedServer];
            }

            if (lastUsedServer == servers.Count - 1)
                wrrCounter = 0; // reset after a full cycle
        }
    }
}