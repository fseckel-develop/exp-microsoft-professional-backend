using LoadBalancingAlgorithmsDemo.Models;

namespace LoadBalancingAlgorithmsDemo.Scenarios;

public static class DemoDatasetFactory
{
    public static DemoDataset CreateGatewayRoutingDataset()
    {
        return new DemoDataset
        {
            Title = "API Gateway Load Balancing Demo",
            Description =
                "Simulate client requests routed to backend catalog services using different load balancing strategies.",

            Backends =
            [
                new BackendInstance("catalog-api-1", weight: 3),
                new BackendInstance("catalog-api-2", weight: 1),
                new BackendInstance("catalog-api-3", weight: 2)
            ],

            Requests =
            [
                new ClientRequest("192.168.1.10", "/api/products"),
                new ClientRequest("192.168.1.11", "/api/products/1"),
                new ClientRequest("192.168.1.12", "/api/products/2"),
                new ClientRequest("192.168.1.13", "/api/categories"),
                new ClientRequest("192.168.1.14", "/api/products"),
                new ClientRequest("192.168.1.15", "/api/search?q=keyboard"),
                new ClientRequest("192.168.1.16", "/api/products/5"),
                new ClientRequest("192.168.1.17", "/api/products/8")
            ]
        };
    }
}