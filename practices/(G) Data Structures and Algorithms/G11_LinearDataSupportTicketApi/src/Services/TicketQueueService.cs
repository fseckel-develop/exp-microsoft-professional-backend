using LinearDataSupportTicketApi.Models;

namespace LinearDataSupportTicketApi.Services;

public sealed class TicketQueueService
{
    private readonly Queue<Guid> _pendingTicketIds = new();
    private readonly object _sync = new();

    public void Enqueue(Ticket ticket)
    {
        lock (_sync)
        {
            _pendingTicketIds.Enqueue(ticket.Id);
        }
    }

    public Guid? Dequeue()
    {
        lock (_sync)
        {
            return _pendingTicketIds.Count > 0
                ? _pendingTicketIds.Dequeue()
                : null;
        }
    }

    public IReadOnlyCollection<Guid> GetAllPendingIds()
    {
        lock (_sync)
        {
            return _pendingTicketIds.ToArray();
        }
    }

    public int Count
    {
        get
        {
            lock (_sync)
            {
                return _pendingTicketIds.Count;
            }
        }
    }
}