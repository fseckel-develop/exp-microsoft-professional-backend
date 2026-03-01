using Microsoft.AspNetCore.Mvc;
using OpenApiSwaggerDemo.Contracts;
using OpenApiSwaggerDemo.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace OpenApiSwaggerDemo.Controllers;

[ApiController]
[Route("controller/cookbooks")]
[Produces("application/json")]
[Tags("Cookbooks (Controller)")]
public sealed class CookbooksController : ControllerBase
{
    private static readonly List<Cookbook> cookbooks =
    [
        new Cookbook
        {
            Id = 1,
            Title = "Weeknight Dinners",
            Author = "Jamie Carter",
            RecipeCount = 24
        },
        new Cookbook
        {
            Id = 2,
            Title = "Healthy Breakfasts",
            Author = "Nina Patel",
            RecipeCount = 18
        }
    ];

    private static int nextCookbookId = 3;

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all cookbooks",
        Description = "Returns the complete list of cookbooks managed by the controller-based API."
    )]
    [ProducesResponseType(typeof(IEnumerable<Cookbook>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Cookbook>> GetAll()
    {
        return Ok(cookbooks.OrderBy(c => c.Id));
    }

    [HttpGet("{id:int}", Name = "GetCookbookById")]
    [SwaggerOperation(
        Summary = "Get a cookbook by ID",
        Description = "Returns a single cookbook by its unique identifier, or 404 if no cookbook exists with that ID."
    )]
    [ProducesResponseType(typeof(Cookbook), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public ActionResult<Cookbook> GetById(
        [FromRoute, SwaggerParameter("The unique ID of the cookbook to retrieve.")] int id)
    {
        var cookbook = cookbooks.FirstOrDefault(c => c.Id == id);

        if (cookbook is null)
            return NotFound("Cookbook not found.");

        return Ok(cookbook);
    }

    [HttpPost]
    [Consumes("application/json")]
    [SwaggerOperation(
        Summary = "Create a new cookbook",
        Description = "Creates a new cookbook resource and returns the created object."
    )]
    [ProducesResponseType(typeof(Cookbook), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public ActionResult<Cookbook> Create([FromBody] CreateCookbookRequestDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
            return BadRequest("Cookbook title is required.");

        if (string.IsNullOrWhiteSpace(dto.Author))
            return BadRequest("Cookbook author is required.");

        if (dto.RecipeCount < 0)
            return BadRequest("Recipe count must be greater than or equal to zero.");

        var cookbook = new Cookbook
        {
            Id = nextCookbookId++,
            Title = dto.Title.Trim(),
            Author = dto.Author.Trim(),
            RecipeCount = dto.RecipeCount
        };

        cookbooks.Add(cookbook);

        return CreatedAtRoute("GetCookbookById", new { id = cookbook.Id }, cookbook);
    }

    [HttpPut("{id:int}")]
    [Consumes("application/json")]
    [SwaggerOperation(
        Summary = "Update an existing cookbook",
        Description = "Updates all editable fields of an existing cookbook by ID."
    )]
    [ProducesResponseType(typeof(Cookbook), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public ActionResult<Cookbook> Update(
        [FromRoute, SwaggerParameter("The unique ID of the cookbook to update.")] int id,
        [FromBody] UpdateCookbookRequestDto dto)
    {
        var cookbook = cookbooks.FirstOrDefault(c => c.Id == id);

        if (cookbook is null)
            return NotFound("Cookbook not found.");

        if (string.IsNullOrWhiteSpace(dto.Title))
            return BadRequest("Cookbook title is required.");

        if (string.IsNullOrWhiteSpace(dto.Author))
            return BadRequest("Cookbook author is required.");

        if (dto.RecipeCount < 0)
            return BadRequest("Recipe count must be greater than or equal to zero.");

        cookbook.Title = dto.Title.Trim();
        cookbook.Author = dto.Author.Trim();
        cookbook.RecipeCount = dto.RecipeCount;

        return Ok(cookbook);
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(
        Summary = "Delete a cookbook",
        Description = "Deletes an existing cookbook by ID and returns the deleted resource."
    )]
    [ProducesResponseType(typeof(Cookbook), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public ActionResult<Cookbook> Delete(
        [FromRoute, SwaggerParameter("The unique ID of the cookbook to delete.")] int id)
    {
        var cookbook = cookbooks.FirstOrDefault(c => c.Id == id);

        if (cookbook is null)
            return NotFound("Cookbook not found.");

        cookbooks.Remove(cookbook);

        return Ok(cookbook);
    }
}