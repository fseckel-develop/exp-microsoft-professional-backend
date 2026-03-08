namespace GraphCampusNavigator.Models;

public sealed record PathResult(
    IReadOnlyList<string> Path,
    int TotalCost,
    bool Found
);