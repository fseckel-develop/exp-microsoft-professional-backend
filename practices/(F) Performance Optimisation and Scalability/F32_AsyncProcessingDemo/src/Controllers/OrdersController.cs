using Microsoft.AspNetCore.Mvc;
using AsyncProcessingDemo.AsyncProcessing;
using AsyncProcessingDemo.Data;
using AsyncProcessingDemo.Models;
using AsyncProcessingDemo.Contracts;

namespace OrderProcessingApiDemo.Controllers;

[ApiController]
[Route("api/orders")]
public sealed class OrdersController : ControllerBase
{
    private readonly OrderStorage _store;
    private readonly IBackgroundTaskQueue _queue;
    private readonly OrderJobProcessor _processor;

    public OrdersController(
        OrderStorage store,
        IBackgroundTaskQueue queue,
        OrderJobProcessor processor)
    {
        _store = store;
        _queue = queue;
        _processor = processor;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateOrderRequestDto dto,
        CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(dto.CustomerName))
            return BadRequest("CustomerName is required.");

        if (string.IsNullOrWhiteSpace(dto.ProductName))
            return BadRequest("ProductName is required.");

        if (dto.Quantity <= 0)
            return BadRequest("Quantity must be greater than zero.");

        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerName = dto.CustomerName,
            ProductName = dto.ProductName,
            Quantity = dto.Quantity,
            CreatedAtUtc = DateTime.UtcNow,
            Status = OrderStatus.Pending
        };

        _store.Add(order);

        await _queue.EnqueueAsync(
            token => _processor.ProcessAsync(order.Id, token),
            ct);

        return AcceptedAtAction(
            nameof(GetById),
            new { id = order.Id },
            new
            {
                message = "Order accepted for background processing.",
                orderId = order.Id,
                status = order.Status
            });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_store.GetAll());
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var order = _store.GetById(id);
        return order is null ? NotFound() : Ok(order);
    }

    [HttpGet("{id:guid}/status")]
    public IActionResult GetStatus(Guid id)
    {
        var order = _store.GetById(id);

        if (order is null)
            return NotFound();

        return Ok(new
        {
            orderId = order.Id,
            status = order.Status,
            createdAtUtc = order.CreatedAtUtc,
            processingStartedAtUtc = order.ProcessingStartedAtUtc,
            completedAtUtc = order.CompletedAtUtc
        });
    }

    [HttpGet("pending")]
    public IActionResult GetPending()
    {
        return Ok(_store.GetByStatus(OrderStatus.Pending));
    }

    [HttpGet("processing")]
    public IActionResult GetProcessing()
    {
        return Ok(_store.GetByStatus(OrderStatus.Processing));
    }

    [HttpGet("completed")]
    public IActionResult GetCompleted()
    {
        return Ok(_store.GetByStatus(OrderStatus.Completed));
    }
}