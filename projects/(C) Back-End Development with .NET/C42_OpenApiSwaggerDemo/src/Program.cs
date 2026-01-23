using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore;
using Microsoft.AspNetCore.Http.HttpResults;


var blogs = new List<Blog>() {
    new Blog { 
        Title = "First Blog Post", 
        Content = "This is the content of the first blog post." },
    new Blog { 
        Title = "Second Blog Post", 
        Content = "This is the content of the second blog post." }
};


var builder = WebApplication.CreateBuilder(args);


// Adding Swagger to the API services:
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


// Using Swagger only in Development to hide API data from Users
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/Swagger", () => {});

app.MapGet("Swagger/v1/swagger.json", () => {});


// Using TypedResults as Return value is a Best Practice at Microsoft
// -> Returntype needs to be specified before the lambda expression
// -> Swagger will use these specified return types for documentation


app.MapGet("/blogs/{index}", Results<Ok<Blog>, NotFound<string>> (int index) =>
{
    if (index < 0 || blogs.Count <= index)
    {
        return TypedResults.NotFound("Blog not found");
    }
    return TypedResults.Ok(blogs[index]);
// Custom Configuration of the OpenApi/Swagger Documentaton
}).WithOpenApi(operation => 
{
    operation.Parameters![0].Description = "The Index of the blog to retrieve.";
    operation.Summary = "Get single blog.";
    operation.Description = "Returns a single blog with the given Index, or none if the Index doesn't exist";
    return operation;
});


// All other Routes will also be displayed by Swagger automatically:
app.MapPost("/blogs", (Blog newBlog) => 
{
    blogs.Add(newBlog);
    return Results.Ok(newBlog);
});

app.MapGet("/blogs", () => {
    return blogs;
});

app.MapPut("/blogs/{index}", (int index, Blog updatedBlog) => 
{
    if (index < 0 || index >= blogs.Count)
    {
        return Results.NotFound("Blog not found");
    }
    blogs[index] = updatedBlog;
    return Results.Ok(updatedBlog);
});

app.MapDelete("/blogs/{index}", (int index) => 
{
    if (index < 0 || index >= blogs.Count)
    {
        return Results.NotFound("Blog not found");
    }
    var deletedBlog = blogs[index];
    blogs.RemoveAt(index);
    return Results.Ok(deletedBlog);
});

app.Run();



public class Blog
{
    public string? Title { get; set; }
    public string? Content { get; set; }
}
