using System.Collections.Concurrent;
using AsyncProcessingDemo.Models;

namespace AsyncProcessingDemo.Data;

public sealed class OrderStorage
{
    private readonly ConcurrentDictionary<Guid, Order> _orders = new();
    private readonly object _sync = new();

    public Order Add(Order order)
    {
        lock (_sync)
        {
            _orders[order.Id] = order;
            return order;
        }
    }

    public Order? GetById(Guid id)
    {
        lock (_sync)
        {
            return _orders.TryGetValue(id, out var order) ? Clone(order) : null;
        }
    }

    public IReadOnlyCollection<Order> GetAll()
    {
        lock (_sync)
        {
            return _orders.Values
                .OrderBy(o => o.CreatedAtUtc)
                .Select(Clone)
                .ToArray();
        }
    }

    public IReadOnlyCollection<Order> GetByStatus(OrderStatus status)
    {
        lock (_sync)
        {
            return _orders.Values
                .Where(o => o.Status == status)
                .OrderBy(o => o.CreatedAtUtc)
                .Select(Clone)
                .ToArray();
        }
    }

    public bool TryUpdate(Guid id, Action<Order> update)
    {
        lock (_sync)
        {
            if (!_orders.TryGetValue(id, out var order))
                return false;

            update(order);
            return true;
        }
    }

    private static Order Clone(Order order) => new()
    {
        Id = order.Id,
        CustomerName = order.CustomerName,
        ProductName = order.ProductName,
        Quantity = order.Quantity,
        CreatedAtUtc = order.CreatedAtUtc,
        Status = order.Status,
        ProcessingStartedAtUtc = order.ProcessingStartedAtUtc,
        CompletedAtUtc = order.CompletedAtUtc
    };
}