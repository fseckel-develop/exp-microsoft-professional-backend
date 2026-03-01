# (C21) BasicCrudApi

A minimal ASP.NET Core Web API demonstrating CRUD operations, routing, and middleware using a simple in-memory task management service.

---
## Course Context

**Course**: Back-End Development with .NET  
**Section**: Building Web APIs and CRUD Operations

This project introduces the fundamentals of building REST-style APIs with ASP.NET Core. It demonstrates how HTTP routes map to application logic, how CRUD operations are implemented, and how middleware participates in the request pipeline.

---
## Concepts Demonstrated

- Building a minimal ASP.NET Core Web API
- HTTP methods (`GET`, `POST`, `PUT`, `PATCH`, `DELETE`)
- CRUD operations for resource management
- Route patterns, parameters, and constraints
- Query parameters and catch-all routes
- Request/response status codes
- Custom middleware and middleware pipeline flow
- DTO usage for API request models

---
## Project Structure

- **Models** – Task data model used by the API  
- **Contracts** – DTOs defining request structures for create/update operations  
- **Middleware** – Custom middleware demonstrating request logging and pipeline flow  
- **Program.cs** – API configuration, routing, middleware registration, and endpoint definitions  
- **Requests.http** - HTTP requests to provided endpoints for testing