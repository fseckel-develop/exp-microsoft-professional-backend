using ControllerDrivenApi.Contracts;
using ControllerDrivenApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControllerDrivenApi.Controllers;

[ApiController]
[Route("controlled/[controller]")]
public sealed class ProductsController : ControllerBase
{
    private static readonly List<Product> products =
    [
        new Product { Id = 1, Name = "Apple", Description = "Fresh red apple", Price = 0.5m },
        new Product { Id = 2, Name = "Banana", Description = "Ripe yellow banana", Price = 0.3m },
        new Product { Id = 3, Name = "Orange", Description = "Juicy orange", Price = 0.4m }
    ];

    private static int nextId = 4;

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll(
        [FromQuery] string? query,
        [FromQuery] decimal? maxPrice)
    {
        IEnumerable<Product> result = products;

        if (!string.IsNullOrWhiteSpace(query))
        {
            result = result.Where(p =>
                p.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                p.Description.Contains(query, StringComparison.OrdinalIgnoreCase));
        }

        if (maxPrice.HasValue)
        {
            result = result.Where(p => p.Price <= maxPrice.Value);
        }

        return Ok(result.OrderBy(p => p.Id).ToList());
    }

    [HttpGet("{id:int}", Name = "GetProductById")]
    public ActionResult<Product> GetById(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);

        if (product is null)
            return NotFound("Product not found.");

        return Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> Create([FromBody] CreateProductRequestDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return BadRequest("Name is required.");

        if (dto.Price < 0)
            return BadRequest("Price must be greater than or equal to zero.");

        var newProduct = new Product
        {
            Id = nextId++,
            Name = dto.Name.Trim(),
            Description = dto.Description?.Trim() ?? string.Empty,
            Price = dto.Price
        };

        products.Add(newProduct);

        return CreatedAtRoute("GetProductById", new { id = newProduct.Id }, newProduct);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Product> Update(int id, [FromBody] UpdateProductRequestDto dto)
    {
        var product = products.FirstOrDefault(p => p.Id == id);

        if (product is null)
            return NotFound("Product not found.");

        if (string.IsNullOrWhiteSpace(dto.Name))
            return BadRequest("Name is required.");

        if (dto.Price < 0)
            return BadRequest("Price must be greater than or equal to zero.");

        product.Name = dto.Name.Trim();
        product.Description = dto.Description?.Trim() ?? string.Empty;
        product.Price = dto.Price;

        return Ok(product);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);

        if (product is null)
            return NotFound("Product not found.");

        products.Remove(product);

        return NoContent();
    }
}