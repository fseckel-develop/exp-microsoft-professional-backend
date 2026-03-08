namespace TreeStructuresDemo.Models;

public sealed class DemoDataset
{
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required ContentCategory RootCategory { get; init; }
    public required ContentItem[] ContentItems { get; init; }
    public required ContentItem[] SearchItems { get; init; }
}