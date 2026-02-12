
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpLogging();

var app = builder.Build();



// Built-In Middleware:
app.UseHttpLogging();       // for logging HTTP requests and responses

// Are already called in the background:
// app.UseRouting();
// app.UseEndpoints(...);
// app.UseAuthorization();
// app.UseAuthentication();
// app.UseExceptionHandler(...);



// Custom Middleware:
app.Use(async (context, next) =>
{
    Console.WriteLine($"\nIncoming Request: {context.Request.Method} {context.Request.Path}\n");
    //
    // Logic before calling the next middleware:
    //
    // e.g. Custom Middleware to log request method and path
    Console.WriteLine("\tMiddleware (1) pre-processing Logic { ...");


    // Call the next middleware in the pipeline
    await next.Invoke();


    Console.WriteLine("\t... } Middleware (1) post-processing Logic");
    // e.g. for clean-up, logging, finalizing, etc.
    // 
    // Logic after the next middleware has processed 
    // 
    Console.WriteLine($"\nOutgoing Response: {context.Response.StatusCode}\n");
});

app.Use(async (context, next) =>
{
    Console.WriteLine("\tMiddleware (2) pre-processing Logic { ...");

    await next.Invoke();

    Console.WriteLine("\t... } Middleware (2) post-processing Logic");
});

app.Use(async (context, next) =>
{
    await next.Invoke(); Console.WriteLine("\n\t\t{ ... Endpoint Logic ... }\n");
});


// Root Route:
app.MapGet("/", () => "Welcome to my First Web API!");




// Dynamic routing using Parameters
//   - ID has constraints (type and range)
//   - Slug is optional parameter
app.MapGet("/user/{id:int:min(0)}/posts/{slug?}", (int id, string? slug) => 
{
    return $"User ID: {id}, Post ID: {slug ?? "No slug provided"}";
});


// Routing with Catch-All Syntax (Wildcard):
app.MapGet("/files/{*filepath}", (string filepath) => 
{
    return $"Filepath: {filepath}";
});


// Query string parameters
app.MapGet("/search/", (string? query, int page = 1) => 
{
    return $"Searching for {query} on page {page}";
});



app.Run();
