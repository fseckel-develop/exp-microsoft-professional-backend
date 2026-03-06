namespace Gateway.Infrastructure;

public sealed class GatewayRouteInfoProvider
{
    private readonly IConfiguration _configuration;

    public GatewayRouteInfoProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public object GetRouteInfo()
    {
        var destinations = _configuration
            .GetSection("ReverseProxy:Clusters:catalogCluster:Destinations")
            .GetChildren()
            .Select(x => new
            {
                id = x.Key,
                address = x["Address"]
            })
            .ToArray();

        return new
        {
            cluster = "catalogCluster",
            loadBalancingPolicy = _configuration["ReverseProxy:Clusters:catalogCluster:LoadBalancingPolicy"],
            destinations
        };
    }
}