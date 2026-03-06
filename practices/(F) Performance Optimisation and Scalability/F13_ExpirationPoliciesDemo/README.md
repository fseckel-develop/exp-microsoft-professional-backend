# (F13) ExpirationPoliciesDemo

A console application demonstrating **different cache expiration strategies** including absolute expiration, sliding expiration, and dependent expiration. The project illustrates how caching systems maintain performance while ensuring data freshness and consistency.

---
## Course Context

**Course:** Performance Optimisation and Scalability  
**Section:** Cache Expiration Policies

This project demonstrates how cache entries can automatically expire or be invalidated based on different policies. It shows how expiration strategies help maintain **data accuracy while preserving the performance benefits of caching**.

---
## Concepts Demonstrated

- Absolute expiration for fixed-lifetime cache entries
- Sliding expiration for frequently accessed data
- Dependent expiration for related cached data
- Using `IMemoryCache` for local caching
- Using Redis TTL for distributed cache expiration
- Designing cache strategies for different data types
- Console-based demonstrations of cache behaviour

---
## Project Structure

- **Models** – Data models used in the caching scenarios  
- **Scenarios** – Individual demonstrations of expiration strategies  
- **Infrastructure** – Cache factory helpers for memory and Redis connections  
- **Presentation** – Console output utilities for displaying scenario results  
- **Program.cs** – Application entry point and scenario runner