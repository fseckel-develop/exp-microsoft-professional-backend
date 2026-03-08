using LinearDataSupportTicketApi.Models;

namespace LinearDataSupportTicketApi.Contracts;

public sealed record UpdateTicketStatusRequestDto(
    TicketStatus NewStatus
);