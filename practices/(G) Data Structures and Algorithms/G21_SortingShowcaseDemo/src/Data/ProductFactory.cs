namespace SortingShowcaseDemo.Data;

public static class ProductFactory
{
    private static readonly string[] ProductNames =
    [
        "Wireless Mouse",
        "Mechanical Keyboard",
        "USB-C Dock",
        "4K Monitor",
        "Laptop Stand",
        "Noise Cancelling Headphones",
        "Webcam",
        "Portable SSD",
        "Desk Lamp",
        "Microphone"
    ];

    public static Product[] CreateRandomCatalog(int count, int seed = 42)
    {
        var random = new Random(seed);
        var products = new Product[count];

        for (int i = 0; i < count; i++)
        {
            var name = ProductNames[random.Next(ProductNames.Length)] + $" #{i + 1}";
            var price = Math.Round((decimal)(random.NextDouble() * 490 + 10), 2);
            var popularity = random.Next(1, 101);

            products[i] = new Product(
                Id: i + 1,
                Name: name,
                Price: price,
                PopularityScore: popularity);
        }

        return products;
    }
}