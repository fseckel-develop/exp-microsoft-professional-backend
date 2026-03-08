/*
HOW TO USE THIS DEMO

This project demonstrates linear data structures inside a small support desk API.

Data structure usage:
- Array: fixed support lanes
- Queue: pending incoming tickets
- Stack: undo for the latest status changes
- LinkedList: timeline/history of events for each ticket

Suggested demo flow:
1. GET    /api/lanes
2. POST   /api/tickets
3. POST   /api/tickets
4. GET    /api/tickets/pending
5. POST   /api/tickets/process-next
6. GET    /api/tickets/{id}
7. GET    /api/tickets/{id}/timeline
8. POST   /api/tickets/{id}/status
9. POST   /api/tickets/undo-last-action

This keeps the API small while making each data structure easy to inspect.
*/

using System.Text.Json.Serialization;
using LinearDataSupportTicketApi.Data;
using LinearDataSupportTicketApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddSingleton<TicketRepository>();
builder.Services.AddSingleton<SupportLaneService>();
builder.Services.AddSingleton<TicketQueueService>();
builder.Services.AddSingleton<TicketUndoService>();
builder.Services.AddSingleton<TicketHistoryService>();
builder.Services.AddSingleton<TicketWorkflowService>();

var app = builder.Build();

app.MapControllers();

app.Run();