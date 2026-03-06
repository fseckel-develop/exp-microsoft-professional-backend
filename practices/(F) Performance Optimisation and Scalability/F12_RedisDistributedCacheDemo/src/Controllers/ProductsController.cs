using Microsoft.AspNetCore.Mvc;
using RedisDistributedCacheDemo.Services;
using RedisDistributedCacheDemo.Data;

namespace RedisDistributedCacheDemo.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _products;
    private readonly IProductRepository _repo;

    public ProductsController(IProductService products, IProductRepository repo)
    {
        _products = products;
        _repo = repo;
    }

    [HttpGet("seed")]
    public IActionResult Seed()
    {
        return Ok(_repo.SeededData);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken ct)
    {
        var product = await _products.GetProductAsync(id, ct);

        if (product is null)
            return NotFound();

        return Ok(product);
    }

    [HttpGet("featured")]
    public async Task<IActionResult> GetFeatured(CancellationToken ct)
    {
        var featured = await _products.GetFeaturedProductsAsync(ct);
        return Ok(featured);
    }

    [HttpDelete("{id:guid}/cache")]
    public async Task<IActionResult> InvalidateProduct(Guid id, CancellationToken ct)
    {
        await _products.InvalidateProductAsync(id, ct);
        return Ok("Product cache invalidated.");
    }

    [HttpDelete("featured/cache")]
    public async Task<IActionResult> InvalidateFeatured(CancellationToken ct)
    {
        await _products.InvalidateFeaturedAsync(ct);
        return Ok("Featured cache invalidated.");
    }
}