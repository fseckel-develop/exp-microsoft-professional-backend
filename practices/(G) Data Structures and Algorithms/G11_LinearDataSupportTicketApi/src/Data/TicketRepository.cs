using System.Collections.Concurrent;
using LinearDataSupportTicketApi.Models;

namespace LinearDataSupportTicketApi.Data;

public sealed class TicketRepository
{
    private readonly ConcurrentDictionary<Guid, Ticket> _tickets = new();

    public Ticket Add(Ticket ticket)
    {
        _tickets[ticket.Id] = ticket;
        return ticket;
    }

    public Ticket? GetById(Guid id)
    {
        _tickets.TryGetValue(id, out var ticket);
        return ticket;
    }

    public IReadOnlyCollection<Ticket> GetAll()
    {
        return _tickets.Values
            .OrderBy(t => t.CreatedAtUtc)
            .ToArray();
    }
}