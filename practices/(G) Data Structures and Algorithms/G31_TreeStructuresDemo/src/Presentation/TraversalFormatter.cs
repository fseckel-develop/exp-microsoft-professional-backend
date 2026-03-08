using TreeStructuresDemo.Models;

namespace TreeStructuresDemo.Presentation;

public static class TraversalFormatter
{
    public static string FormatItems(IEnumerable<ContentItem> items)
        => string.Join(" -> ", items.Select(x => $"{x.Id}:{x.Title}"));

    public static string FormatCategories(IEnumerable<ContentCategory> categories)
        => string.Join(" -> ", categories.Select(x => $"{x.Id}:{x.Name}"));
}