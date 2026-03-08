namespace SortingShowcaseDemo.Data;

public sealed record Product(
    int Id,
    string Name,
    decimal Price,
    int PopularityScore
);