# (F11) InMemoryCacheDemo

An ASP.NET Core Web API demonstrating **in-memory caching with `IMemoryCache`** to improve application performance. The project shows how frequently accessed data can be cached, retrieved efficiently, and invalidated when necessary.

---
## Course Context

**Course:** Performance Optimisation and Scalability
**Section:** In-Memory Caching

This project demonstrates how **in-memory caching** can reduce database calls and improve response times. It also introduces practical cache management concepts such as cache expiration, cache invalidation, and preventing cache stampedes.

---
## Concepts Demonstrated

- In-memory caching using `IMemoryCache`
- Cache key design for efficient data retrieval
- Cache expiration strategies (absolute and sliding)
- Cache invalidation and manual cache clearing
- Preventing cache stampedes using per-key locking
- Reducing simulated database calls through caching
- Service layering with repository, service, and caching decorators

---
## Project Structure

- **Controllers** – API endpoints demonstrating caching behaviour  
- **Models** – Domain models used in the caching examples  
- **Services** – Business logic for retrieving and caching product data  
- **Data** – Data repository implementation and cache-enabled service wrapper  
- **Caching** – Cache keys, cache options, and stampede protection utilities  
- **Program.cs** – Application startup and dependency configuration
- **Requests.http** - HTTP requests to provided endpoints for testing