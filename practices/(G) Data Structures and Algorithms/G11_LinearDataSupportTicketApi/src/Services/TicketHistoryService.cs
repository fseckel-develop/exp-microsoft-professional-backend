using LinearDataSupportTicketApi.Models;

namespace LinearDataSupportTicketApi.Services;

public sealed class TicketHistoryService
{
    public void AddEvent(Ticket ticket, string description)
    {
        ticket.Timeline.AddLast(new TicketEvent(DateTime.UtcNow, description));
    }

    public IReadOnlyCollection<TicketEvent> GetTimeline(Ticket ticket)
    {
        return ticket.Timeline.ToArray();
    }
}