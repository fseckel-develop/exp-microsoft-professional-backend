/*
HOW TO USE THIS DEMO

This API demonstrates queued background processing.

Flow:
1. POST /api/orders
   - Creates a new order
   - Stores it immediately with status "Pending"
   - Enqueues background processing
   - Returns right away

2. The background worker picks up queued jobs
   - Marks the order as "Processing"
   - Simulates fulfillment work
   - Marks the order as "Completed"

3. Use GET /api/orders or GET /api/orders/{id}
   to observe order state transitions.

This demonstrates how APIs can remain responsive while long-running work
is handled asynchronously in the background.
*/

using AsyncProcessingDemo.AsyncProcessing;
using AsyncProcessingDemo.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<OrderStorage>();
builder.Services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
builder.Services.AddSingleton<OrderJobProcessor>();
builder.Services.AddHostedService<QueuedOrderProcessor>();

var app = builder.Build();

app.MapControllers();

app.Run();