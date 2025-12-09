using Microsoft.AspNetCore.Mvc;
using ControllerDrivenApi.Models;

namespace ControllerDrivenApi.Controllers
{

    [ApiController] [Route("controlled/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Apple", Description = "Fresh red apple", Price = 0.5m },
            new Product { Id = 2, Name = "Banana", Description = "Ripe yellow banana", Price = 0.3m },
            new Product { Id = 3, Name = "Orange", Description = "Juicy orange", Price = 0.4m }
        };


        [HttpGet]
        public ActionResult<List<Product>> GetAll() => products;

        [HttpGet("{id}")]
        public ActionResult<Product> GetByID(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            return product;
        }


        [HttpPost]
        public ActionResult<string> Create(Product newProduct)
        {
            newProduct.Id = products.Count + 1;
            products.Add(newProduct);
            return CreatedAtAction(nameof(GetByID), new { id = newProduct.Id }, newProduct);
        }


        [HttpPut("{id}")]
        public ActionResult<string> Update(int id, Product updatedProduct)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound("Product not found");
            product.Name = updatedProduct.Name;
            product.Description = updatedProduct.Description;
            product.Price = updatedProduct.Price;
            return Ok(product);
        }


        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound("Product not found");
            products.Remove(product);
            return NoContent();
        }
    }
}
