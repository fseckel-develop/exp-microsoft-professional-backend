using Microsoft.AspNetCore.Mvc;
using Gateway.Infrastructure;

namespace Gateway.Controllers;

[ApiController]
[Route("")]
public sealed class GatewayController : ControllerBase
{
    private readonly GatewayRouteInfoProvider _routeInfoProvider;

    public GatewayController(GatewayRouteInfoProvider routeInfoProvider)
    {
        _routeInfoProvider = routeInfoProvider;
    }

    [HttpGet]
    public IActionResult Root()
    {
        return Ok(new
        {
            message = "Gateway is running",
            proxy = "YARP",
            servedAtUtc = DateTime.UtcNow
        });
    }

    [HttpGet("gateway/routes")]
    public IActionResult Routes()
    {
        return Ok(_routeInfoProvider.GetRouteInfo());
    }
}