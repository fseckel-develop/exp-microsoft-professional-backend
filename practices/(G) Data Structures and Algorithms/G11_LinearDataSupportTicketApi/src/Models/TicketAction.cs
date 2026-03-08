namespace LinearDataSupportTicketApi.Models;

public sealed record TicketAction(
    Guid TicketId,
    TicketStatus PreviousStatus,
    TicketStatus NewStatus,
    DateTime ChangedAtUtc
);