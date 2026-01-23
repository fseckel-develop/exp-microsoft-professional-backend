using System.Reflection.Metadata;

public class DownloadClient
{
    public async Task<string> DownloadFileAsync(string fileName)
    {
        Console.WriteLine($"Starting download of {fileName}...");
        await Task.Delay(3000);
        Console.WriteLine($"Completed download of {fileName}.");
        return $"{fileName} content";
    }

    public async Task DownloadFilesAsync()
    {
        var downloadTask1 = DownloadFileAsync("File1.txt");
        var downloadTask2 = DownloadFileAsync("File2.txt");
        await  Task.WhenAll(downloadTask1, downloadTask2);
        Console.WriteLine("All downloads completed.\n");
    }
}

public class Databank
{
    public async Task ProcessDataChunkAsync(int chunkNumber)
    {
        Console.WriteLine($"Processing chunk {chunkNumber}...");
        await Task.Delay(1000);
        Console.WriteLine($"Completed processing of chunk {chunkNumber}.");
    }

    public async Task ProcessLargeDatasetAsync(int numberOfChunks)
    {
        var tasks = new List<Task>();
        for (int i = 1; i <= numberOfChunks; i++)
        {
            tasks.Add(ProcessDataChunkAsync(i));
        }
        await Task.WhenAll(tasks);
        Console.WriteLine("All data chunks processed.\n");
    }
}

public class Product
{
    public string Name { get; set; }

    public Product(string name)
    {
        Name = name;
    }
}

public class Review
{
    public string Content { get; set; }

    public Review(string content)
    {
        Content = content;
    }
}

public class OnlineShop
{
        public async Task<List<Product>> FetchProductsAsync()
    {
        await Task.Delay(2000);
        return new List<Product>
        {
            new Product("Baked Camembert"),
            new Product("Pizza")
        };
    }

    public async Task<List<Review>> FetchReviewsAsync()
    {
        await Task.Delay(3000);
        return new List<Review>
        {
            new Review("Great product!"),
            new Review("Good value for the money."),
        };
    }

    public async Task DisplayDataAsync()
    {
        var productsTask = FetchProductsAsync();
        var reviewsTask = FetchReviewsAsync();
        await Task.WhenAll(productsTask, reviewsTask);

        List<Product> products = await productsTask;
        List<Review> reviews = await reviewsTask;

        PrintProducts(ref products);
        PrintReviews(ref reviews);
    }

    public void PrintProducts(ref List<Product> products)
    {
        Console.WriteLine("Products:");
        foreach (Product product in products)
        {
            Console.WriteLine(" - " + product.Name);
        }
        Console.WriteLine();
    }

    public void PrintReviews(ref List<Review> reviews)
    {
        Console.WriteLine("Reviews:");
        foreach (Review review in reviews)
        {
            Console.WriteLine(review.Content);
        }
        Console.WriteLine();
    }
}

public class BuggyService
{
    public async Task PerformLongOperationAsync()
    {
        try
        {
            Console.WriteLine("Operation started...");
            await Task.Delay(3000);
            throw new InvalidOperationException("Simulated long operation error.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

public class Programm
{
    public static async Task Main(string[] args)
    {
        var downloadClient = new DownloadClient();
        await downloadClient.DownloadFilesAsync();

        var databank = new Databank();
        await databank.ProcessLargeDatasetAsync(10);

        var onlineShop = new OnlineShop();
        await onlineShop.DisplayDataAsync();

        var service = new BuggyService();
        await service.PerformLongOperationAsync();
    }
}