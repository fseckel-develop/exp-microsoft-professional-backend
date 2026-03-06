using Microsoft.AspNetCore.Mvc;
using CatalogApi.Infrastructure;

namespace CatalogApi.Controllers;

[ApiController]
[Route("api/products")]
public sealed class ProductsController : ControllerBase
{
    private readonly ProductCatalog _catalog;
    private readonly InstanceContext _instance;

    public ProductsController(ProductCatalog catalog, InstanceContext instance)
    {
        _catalog = catalog;
        _instance = instance;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _catalog.GetAll();

        return Ok(new
        {
            instance = _instance.InstanceName,
            count = products.Count,
            items = products,
            servedAtUtc = _instance.UtcNow
        });
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var product = _catalog.GetById(id);

        if (product is null)
        {
            return NotFound(new
            {
                message = $"Product with id {id} was not found.",
                instance = _instance.InstanceName,
                servedAtUtc = _instance.UtcNow
            });
        }

        return Ok(new
        {
            instance = _instance.InstanceName,
            item = product,
            servedAtUtc = _instance.UtcNow
        });
    }
}