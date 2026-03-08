namespace TreeStructuresDemo.Models;

public sealed record SearchResult<T>(
    T? Value,
    bool Found,
    int Steps
);