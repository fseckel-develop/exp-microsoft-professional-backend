namespace LinearDataSupportTicketApi.Contracts;

public sealed record CreateTicketRequestDto(
    string CustomerName,
    string Subject,
    string SupportLane
);