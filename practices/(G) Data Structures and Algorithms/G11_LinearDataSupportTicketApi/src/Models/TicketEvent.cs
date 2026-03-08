namespace LinearDataSupportTicketApi.Models;

public sealed record TicketEvent(
    DateTime OccurredAtUtc,
    string Description
);