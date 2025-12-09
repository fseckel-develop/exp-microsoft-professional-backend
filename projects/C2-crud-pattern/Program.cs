
var blogs = new List<Blog>() {
    new Blog { 
        Title = "First Blog Post", 
        Content = "This is the content of the first blog post." },
    new Blog { 
        Title = "Second Blog Post", 
        Content = "This is the content of the second blog post." }
};



var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();



// (C)REATE:
app.MapPost("/blogs", (Blog newBlog) => 
{
    blogs.Add(newBlog);
    return Results.Created($"/blogs/{blogs.Count - 1}", newBlog);
});


// (R)EAD:
app.MapGet("/blogs", () => {
    return blogs;
});

app.MapGet("/blogs/{index}", (int index) => 
{
    if (index < 0 || blogs.Count <= index)
    {
        return Results.NotFound("Blog not found");
    }
    return Results.Ok(blogs[index]);
});


// (U)PDATE:
app.MapPut("/blogs/{index}", (int index, Blog updatedBlog) => 
{
    if (index < 0 || index >= blogs.Count)
    {
        return Results.NotFound("Blog not found");
    }
    blogs[index] = updatedBlog;
    return Results.Ok(updatedBlog);
});


// (D)ELETE:
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
