namespace LinearDataSupportTicketApi.Models;

public sealed class Ticket
{
    public Guid Id { get; init; }
    public required string CustomerName { get; init; }
    public required string Subject { get; init; }
    public required string SupportLane { get; init; }
    public DateTime CreatedAtUtc { get; init; }
    public TicketStatus Status { get; set; }

    // LinkedList demonstration
    public LinkedList<TicketEvent> Timeline { get; } = new();
}