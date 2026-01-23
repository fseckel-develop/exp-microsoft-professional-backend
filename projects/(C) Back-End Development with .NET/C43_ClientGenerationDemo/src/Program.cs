// App running at http://localhost:5221

using BlogApi; // included after Gneration


public class Program
{
    public static async Task Main(string[] args)
    {
        await UseGeneratedClient();
    }

    // Assisted Generation of API Client:
    public static async Task NSwagClientGeneration()
    {
        await new SwaggerClientGenerator().GenerateClient();    // creates BlogApiClient.cs
    }

    // Usage of Generated Client: 
    // shorter, safer and easier to maintain
    public static async Task UseGeneratedClient()
    {
        var httpClient = new HttpClient();
        var apiBaseURL = "http://localhost:5221";
        var blogClient = new BlogApiClient(apiBaseURL, httpClient);

        // CREATE
        var newBlog = new BlogApi.Blog
        {
            Title = "Another Blog",
            Content = "Content of this new Blog"
        };
        await blogClient.BlogsPOSTAsync(newBlog);

        // READ
        var blogs = await blogClient.BlogsAllAsync();
        foreach (var blog in blogs)
        {
            Console.WriteLine($"{blog.Title}: {blog.Content}");
        }

        // DELETE
        await blogClient.BlogsDELETEAsync(0);

        // READ again
        blogs = await blogClient.BlogsAllAsync();
        foreach (var blog in blogs)
        {
            Console.WriteLine($"{blog.Title}: {blog.Content}");
        }
    }

    // Manual Client Management without NSwag Assistance:
    public static async Task ManualClientManagement()
    {
        var httpClient = new HttpClient();
        var apiBaseURL = "http://localhost:5221";

        var httpResult = await httpClient.GetAsync($"{apiBaseURL}/blogs");
        if (httpResult.StatusCode != System.Net.HttpStatusCode.OK)
        {
            Console.WriteLine("Failed to fetch blogs");
            return;
        }

        var blogStream = await httpResult.Content.ReadAsStreamAsync();
        var options = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var blogs = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Blog>>(blogStream, options);
        if (blogs != null)
        {
            foreach (var blog in blogs)
            {
                Console.WriteLine($"{blog.Title}: {blog.Content}");
            }
        }
    }
}



public class Blog
{
    public string? Title { get; set; }
    public string? Content { get; set; }
}
