# (F12) RedisDistributedCacheDemo

An ASP.NET Core Web API demonstrating **distributed caching with Redis** via `IDistributedCache` to improve performance and share cached data across application instances.

---
## Course Context

**Course**: Performance Optimisation and Scalability  
**Section**: Distributed Caching with Redis

This project extends caching concepts beyond single-server memory storage by using **Redis as a centralized distributed cache**. It demonstrates how cached data can be shared across multiple servers, reducing repeated data access and improving scalability.

---
## Concepts Demonstrated

- Distributed caching with Redis
- Using `IDistributedCache` in ASP.NET Core
- Cache hit/miss handling with fallback to a data source
- Cache expiration with absolute and sliding policies
- Serializing cached objects as JSON
- Cache invalidation for individual items and collections
- Shared caching for multi-server scenarios

---
## Project Structure

- **Controllers** – API endpoints demonstrating Redis cache operations and cached product retrieval  
- **Models** – Domain models used in the caching examples  
- **Services** – Business logic for retrieving product data with a distributed cache wrapper  
- **Data** – Repository layer simulating the underlying data source  
- **Caching** – Cache keys, cache options, and JSON serialization helpers  
- **Program.cs** – Application startup and Redis cache configuration  
- **Requests.http** - HTTP requests to provided endpoints for testing