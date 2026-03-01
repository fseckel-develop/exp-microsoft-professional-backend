using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Models;
using OpenApiSwaggerDemo.Contracts;
using OpenApiSwaggerDemo.Models;

var recipes = new List<Recipe>
{
    new()
    {
        Id = 1,
        Name = "Pasta Primavera",
        Instructions = "Boil pasta, sauté vegetables, mix together."
    },
    new()
    {
        Id = 2,
        Name = "Avocado Toast",
        Instructions = "Toast bread, smash avocado, season, serve."
    }
};

var nextRecipeId = recipes.Max(r => r.Id) + 1;

var builder = WebApplication.CreateBuilder(args);

// Add Swagger-enhanced Controller
builder.Services.AddControllers();

// Register OpenAPI/Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Swagger API Demo",
        Version = "v1",
        Description = "A minimal API demo showing OpenAPI/Swagger documentation for CRUD endpoints."
    });
});

var app = builder.Build();

// Expose Swagger UI in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Root
app.MapGet("/", () => Results.Ok(new
{
    message = "Swagger demo is running. Open /swagger to inspect the API documentation."
}))
.WithName("GetRoot")
.WithSummary("Get API root message")
.WithDescription("Returns a simple message pointing users to the Swagger UI.")
.WithOpenApi();

var recipeRoutes = app.MapGroup("/recipes").WithTags("Recipes (Minimal API)");

recipeRoutes.MapGet("/", () =>
{
    return TypedResults.Ok(recipes.OrderBy(r => r.Id));
})
.WithName("GetAllRecipes")
.WithSummary("Get all recipes")
.WithDescription("Returns the full collection of recipes.")
.WithOpenApi();

//
// CRUD Endpoints
//

// Create
recipeRoutes.MapPost(
    "/",
    Results<Created<Recipe>, BadRequest<string>> (CreateRecipeRequestDto dto) =>
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return TypedResults.BadRequest("Recipe name is required.");

        if (string.IsNullOrWhiteSpace(dto.Instructions))
            return TypedResults.BadRequest("Recipe instructions are required.");

        var recipe = new Recipe
        {
            Id = nextRecipeId++,
            Name = dto.Name.Trim(),
            Instructions = dto.Instructions.Trim()
        };

        recipes.Add(recipe);

        return TypedResults.Created($"/recipes/{recipe.Id}", recipe);
    })
.WithName("CreateRecipe")
.WithSummary("Create a recipe")
.WithDescription("Creates a new recipe and returns the created resource.")
.WithOpenApi();

// Read
recipeRoutes.MapGet(
    "/{id:int}",
    Results<Ok<Recipe>, NotFound<string>> (int id) =>
    {
        var recipe = recipes.FirstOrDefault(r => r.Id == id);

        if (recipe is null)
            return TypedResults.NotFound("Recipe not found.");

        return TypedResults.Ok(recipe);
    })
.WithName("GetRecipeById")
.WithSummary("Get a single recipe")
.WithDescription("Returns a recipe by ID, or 404 if the recipe does not exist.")
.WithOpenApi(operation =>
{
    operation.Parameters[0].Description = "The unique ID of the recipe.";
    return operation;
});

// Update
recipeRoutes.MapPut(
    "/{id:int}",
    Results<Ok<Recipe>, NotFound<string>, BadRequest<string>> (int id, UpdateRecipeRequestDto dto) =>
    {
        var recipe = recipes.FirstOrDefault(r => r.Id == id);

        if (recipe is null)
            return TypedResults.NotFound("Recipe not found.");

        if (string.IsNullOrWhiteSpace(dto.Name))
            return TypedResults.BadRequest("Recipe name is required.");

        if (string.IsNullOrWhiteSpace(dto.Instructions))
            return TypedResults.BadRequest("Recipe instructions are required.");

        recipe.Name = dto.Name.Trim();
        recipe.Instructions = dto.Instructions.Trim();

        return TypedResults.Ok(recipe);
    })
.WithName("UpdateRecipe")
.WithSummary("Update a recipe")
.WithDescription("Updates an existing recipe by ID.")
.WithOpenApi();

// Delete
recipeRoutes.MapDelete(
    "/{id:int}",
    Results<Ok<Recipe>, NotFound<string>> (int id) =>
    {
        var recipe = recipes.FirstOrDefault(r => r.Id == id);

        if (recipe is null)
            return TypedResults.NotFound("Recipe not found.");

        recipes.Remove(recipe);

        return TypedResults.Ok(recipe);
    })
.WithName("DeleteRecipe")
.WithSummary("Delete a recipe")
.WithDescription("Deletes an existing recipe by ID.")
.WithOpenApi();

app.MapControllers();

app.Run();