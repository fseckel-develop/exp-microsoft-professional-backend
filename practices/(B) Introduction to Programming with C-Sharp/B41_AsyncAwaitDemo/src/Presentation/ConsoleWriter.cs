using AsyncAwaitDemo.Models;

namespace AsyncAwaitDemo.Presentation;

public sealed class ConsoleWriter
{
    public void WriteTitle(string title)
    {
        Console.WriteLine(title);
        Console.WriteLine(new string('=', title.Length));
        Console.WriteLine();
    }

    public void WriteSection(string title, string description)
    {
        Console.WriteLine(title);
        Console.WriteLine(new string('-', title.Length));
        Console.WriteLine(description);
        Console.WriteLine();
    }

    public void WriteMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void WriteProducts(IEnumerable<Product> products)
    {
        Console.WriteLine("Products:");
        foreach (var product in products)
        {
            Console.WriteLine($" - {product.Name}");
        }

        Console.WriteLine();
    }

    public void WriteReviews(IEnumerable<Review> reviews)
    {
        Console.WriteLine("Reviews:");
        foreach (var review in reviews)
        {
            Console.WriteLine($" - {review.Content}");
        }

        Console.WriteLine();
    }

    public void WriteDashboardData(DashboardData data)
    {
        WriteProducts(data.Products);
        WriteReviews(data.Reviews);
    }

    public void WriteSpacer()
    {
        Console.WriteLine();
        Console.WriteLine(new string('-', 50));
        Console.WriteLine();
    }
}