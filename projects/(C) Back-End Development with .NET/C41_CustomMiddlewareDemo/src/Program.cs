
var blogs = new List<Blog>() {
    new Blog { 
        Title = "First Blog Post", 
        Content = "This is the content of the first blog post." },
    new Blog { 
        Title = "Second Blog Post", 
        Content = "This is the content of the second blog post." }
};

var builder = WebApplication.CreateBuilder(args);


// Configure Services
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();


// Build App with Services
var app = builder.Build();


// Configure Built-In Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
else
{
    app.UseDeveloperExceptionPage();
}
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpLogging();




// Custom Middleware to log Request Processing Time:
app.Use(async (context, next) =>
{
    var startTime = DateTime.UtcNow;
    await next.Invoke();
    var processingTime = DateTime.UtcNow - startTime;
    Console.WriteLine($"Request processed in {processingTime.TotalMilliseconds} ms");
});


// Custom Middleware to log Incoming Requests and Outgoing Responses:
app.Use(async (context, next) =>
{
    // (1) Logic before the next middleware
    Console.WriteLine($"Incoming request: {context.Request.Method} {context.Request.Path}");

    // (2) Call to the next middleware
    await next.Invoke(); 

    // (3) Logic after the next middleware
    Console.WriteLine($"Outgoing response: {context.Response.StatusCode}\n");
});


// Custom Middleware for Password Authentication:
app.UseWhen(
    context => context.Request.Method != "GET",
    // Defining Logic which will run when upper condition is met:
    appBuilder => appBuilder.Use(async (context, next) =>
    {
        var extractedPassword = context.Request.Headers["X-Api-Key"];
        // (Password SHOULD be saved in an external file (environment variable))
        if (extractedPassword == "thisIsABadPassword") 
        {
            await next.Invoke();
        } 
        else
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid API Key");
        }
    })
);





app.MapPost("/blogs", (Blog newBlog) => 
{
    blogs.Add(newBlog);
    return Results.Created($"/blogs/{blogs.Count - 1}", newBlog);
});

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
