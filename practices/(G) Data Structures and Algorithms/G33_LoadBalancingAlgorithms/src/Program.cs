class Program
{
    static void Main()
    {
        // Initialize load balancer
        LoadBalancer loadBalancer = new LoadBalancer();

        // Register servers with optional weights for WRR
        loadBalancer.RegisterServer(new Server("Server A", weight: 3));
        loadBalancer.RegisterServer(new Server("Server B", weight: 1));
        loadBalancer.RegisterServer(new Server("Server C", weight: 2));

        List<string> clientIPs = new List<string>
        {
            "192.168.1.1",
            "192.168.1.2",
            "192.168.1.3",
            "192.168.1.4",
            "192.168.1.5",
            "192.168.1.6",
            "192.168.1.7"
        };

        // Helper method to run requests using any distribution method
        void RunRequests(string title, Func<string, Server> strategy)
        {
            Console.WriteLine($"\n{title}:");
            foreach (var ip in clientIPs)
            {
                var server = strategy(ip);
                server.HandleRequest();
                Console.WriteLine($"Request from {ip} → {server.Id}");
            }
        }

        // Round-Robin
        RunRequests("=== Round-Robin Distribution ===", ip => loadBalancer.GetServerRoundRobin());

        // Least Connections
        RunRequests("=== Least Connections Distribution ===", ip => loadBalancer.GetServerLeastConnections());

        // IP Hashing
        RunRequests("=== IP Hashing Distribution ===", loadBalancer.GetServerIpHashing);

        // Weighted Round-Robin
        RunRequests("=== Weighted Round-Robin Distribution ===", ip => loadBalancer.GetServerWeightedRoundRobin());
    }
}