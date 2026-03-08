namespace KnapsackMemoizationDemo.Models;

public sealed record KnapsackResult(
    int BestValue,
    long CallCount
);