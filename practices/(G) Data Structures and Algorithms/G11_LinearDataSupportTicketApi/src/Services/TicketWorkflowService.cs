using LinearDataSupportTicketApi.Data;
using LinearDataSupportTicketApi.Models;
using LinearDataSupportTicketApi.Contracts;

namespace LinearDataSupportTicketApi.Services;

public sealed class TicketWorkflowService
{
    private readonly TicketRepository _repository;
    private readonly TicketQueueService _queue;
    private readonly TicketUndoService _undo;
    private readonly TicketHistoryService _history;
    private readonly SupportLaneService _lanes;

    public TicketWorkflowService(
        TicketRepository repository,
        TicketQueueService queue,
        TicketUndoService undo,
        TicketHistoryService history,
        SupportLaneService lanes)
    {
        _repository = repository;
        _queue = queue;
        _undo = undo;
        _history = history;
        _lanes = lanes;
    }

    public (bool Success, string? Error, Ticket? Ticket) CreateTicket(CreateTicketRequestDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.CustomerName))
            return (false, "CustomerName is required.", null);

        if (string.IsNullOrWhiteSpace(dto.Subject))
            return (false, "Subject is required.", null);

        if (string.IsNullOrWhiteSpace(dto.SupportLane))
            return (false, "SupportLane is required.", null);

        if (!_lanes.Exists(dto.SupportLane))
            return (false, $"Support lane '{dto.SupportLane}' does not exist.", null);

        var ticket = new Ticket
        {
            Id = Guid.NewGuid(),
            CustomerName = dto.CustomerName.Trim(),
            Subject = dto.Subject.Trim(),
            SupportLane = dto.SupportLane.Trim(),
            CreatedAtUtc = DateTime.UtcNow,
            Status = TicketStatus.Pending
        };

        _history.AddEvent(ticket, $"Ticket created in lane '{ticket.SupportLane}'.");
        _history.AddEvent(ticket, "Ticket added to pending queue.");

        _repository.Add(ticket);
        _queue.Enqueue(ticket);

        return (true, null, ticket);
    }

    public Ticket? ProcessNextTicket()
    {
        var ticketId = _queue.Dequeue();
        if (ticketId is null)
            return null;

        var ticket = _repository.GetById(ticketId.Value);
        if (ticket is null)
            return null;

        var previousStatus = ticket.Status;
        ticket.Status = TicketStatus.InProgress;

        _undo.Push(new TicketAction(
            ticket.Id,
            previousStatus,
            ticket.Status,
            DateTime.UtcNow));

        _history.AddEvent(ticket, "Ticket dequeued for handling.");
        _history.AddEvent(ticket, $"Status changed from {previousStatus} to {ticket.Status}.");

        return ticket;
    }

    public (bool Success, string? Error, Ticket? Ticket) ChangeStatus(Guid ticketId, TicketStatus newStatus)
    {
        var ticket = _repository.GetById(ticketId);
        if (ticket is null)
            return (false, "Ticket not found.", null);

        var previousStatus = ticket.Status;
        ticket.Status = newStatus;

        _undo.Push(new TicketAction(
            ticket.Id,
            previousStatus,
            newStatus,
            DateTime.UtcNow));

        _history.AddEvent(ticket, $"Status changed from {previousStatus} to {newStatus}.");

        return (true, null, ticket);
    }

    public (bool Success, string? Error, Ticket? Ticket, TicketAction? RevertedAction) UndoLastAction()
    {
        var action = _undo.Pop();
        if (action is null)
            return (false, "No action available to undo.", null, null);

        var ticket = _repository.GetById(action.TicketId);
        if (ticket is null)
            return (false, "Associated ticket was not found.", null, action);

        ticket.Status = action.PreviousStatus;
        _history.AddEvent(ticket, $"Undo applied: status reverted from {action.NewStatus} to {action.PreviousStatus}.");

        if (ticket.Status == TicketStatus.Pending)
            _queue.Enqueue(ticket);

        return (true, null, ticket, action);
    }

    public IReadOnlyCollection<Ticket> GetPendingTickets()
    {
        var ids = _queue.GetAllPendingIds();

        return ids
            .Select(id => _repository.GetById(id))
            .Where(ticket => ticket is not null)
            .Cast<Ticket>()
            .ToArray();
    }
}