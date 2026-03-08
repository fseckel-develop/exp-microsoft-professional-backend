using KnapsackMemoizationDemo.Models;

namespace KnapsackMemoizationDemo.Algorithms;

public interface IKnapsackSolver
{
    string Name { get; }
    KnapsackResult Solve(IReadOnlyList<SupplyItem> items, int capacity);
}