using Microsoft.AspNetCore.Mvc;
using LinearDataSupportTicketApi.Services;

namespace LinearDataSupportTicketApi.Controllers;

[ApiController]
[Route("api/lanes")]
public sealed class LanesController : ControllerBase
{
    private readonly SupportLaneService _lanes;

    public LanesController(SupportLaneService lanes)
    {
        _lanes = lanes;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(new
        {
            dataStructure = "Array",
            count = _lanes.GetAll().Count,
            items = _lanes.GetAll()
        });
    }
}