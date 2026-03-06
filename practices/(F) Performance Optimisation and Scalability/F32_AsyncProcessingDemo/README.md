# (F32) AsyncProcessingDemo

An ASP.NET Core Web API demonstrating **asynchronous background processing with a queued workload**. Orders are accepted immediately, stored with an initial status, and then processed asynchronously in the background without blocking the API response.

---
## Course Context

**Course**: Performance Optimisation and Scalability  
**Section**: Asynchronous Processing

This project demonstrates how long-running work can be moved out of the main request pipeline into a background queue. It shows how APIs can remain responsive while queued jobs are processed asynchronously and order status changes are tracked over time.

---
## Concepts Demonstrated

- Asynchronous background processing
- Queue-based workload handling
- Hosted background services in ASP.NET Core
- Non-blocking API design
- Status-based workflow tracking
- Concurrent in-memory storage for queued jobs
- Dependency injection for background processors and services

---
## Project Structure

- **AsyncProcessing** – Background queue, job processor, and hosted worker service  
- **Contracts** – Request DTOs for creating orders  
- **Controllers** – API endpoints for orders and system health  
- **Data** – In-memory order storage  
- **Models** – Order domain model and status definitions  
- **Program.cs** – Application startup and background service registration  
- **Requests.http** - HTTP requests to provided endpoints for testing
