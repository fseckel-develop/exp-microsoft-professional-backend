using Microsoft.AspNetCore.Mvc;
using LinearDataSupportTicketApi.Data;
using LinearDataSupportTicketApi.Services;
using LinearDataSupportTicketApi.Contracts;

namespace LinearDataSupportTicketApi.Controllers;

[ApiController]
[Route("api/tickets")]
public sealed class TicketsController : ControllerBase
{
    private readonly TicketRepository _repository;
    private readonly TicketWorkflowService _workflow;
    private readonly TicketHistoryService _history;
    private readonly TicketQueueService _queue;
    private readonly TicketUndoService _undo;

    public TicketsController(
        TicketRepository repository,
        TicketWorkflowService workflow,
        TicketHistoryService history,
        TicketQueueService queue,
        TicketUndoService undo)
    {
        _repository = repository;
        _workflow = workflow;
        _history = history;
        _queue = queue;
        _undo = undo;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateTicketRequestDto dto)
    {
        var result = _workflow.CreateTicket(dto);

        if (!result.Success)
            return BadRequest(result.Error);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Ticket!.Id },
            new
            {
                message = "Ticket created and added to pending queue.",
                ticketId = result.Ticket.Id,
                status = result.Ticket.Status
            });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_repository.GetAll());
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var ticket = _repository.GetById(id);
        return ticket is null ? NotFound() : Ok(ticket);
    }

    [HttpGet("pending")]
    public IActionResult GetPending()
    {
        return Ok(new
        {
            dataStructure = "Queue",
            count = _queue.Count,
            items = _workflow.GetPendingTickets()
        });
    }

    [HttpPost("process-next")]
    public IActionResult ProcessNext()
    {
        var ticket = _workflow.ProcessNextTicket();

        if (ticket is null)
            return NotFound("No pending ticket is available.");

        return Ok(new
        {
            message = "Next pending ticket processed.",
            ticket
        });
    }

    [HttpPost("{id:guid}/status")]
    public IActionResult ChangeStatus(Guid id, [FromBody] UpdateTicketStatusRequestDto dto)
    {
        var result = _workflow.ChangeStatus(id, dto.NewStatus);

        if (!result.Success)
            return NotFound(result.Error);

        return Ok(new
        {
            message = "Ticket status updated.",
            ticket = result.Ticket
        });
    }

    [HttpPost("undo-last-action")]
    public IActionResult UndoLastAction()
    {
        var result = _workflow.UndoLastAction();

        if (!result.Success)
            return NotFound(result.Error);

        return Ok(new
        {
            dataStructure = "Stack",
            message = "Last ticket action undone.",
            revertedAction = result.RevertedAction,
            ticket = result.Ticket,
            remainingUndoActions = _undo.Count
        });
    }

    [HttpGet("{id:guid}/timeline")]
    public IActionResult GetTimeline(Guid id)
    {
        var ticket = _repository.GetById(id);
        if (ticket is null)
            return NotFound();

        return Ok(new
        {
            dataStructure = "LinkedList",
            ticketId = ticket.Id,
            events = _history.GetTimeline(ticket)
        });
    }
}