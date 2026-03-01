# (C41) MiddlewarePipelineDemo

An ASP.NET Core API demonstrating how the middleware pipeline processes HTTP requests, including logging, validation, authentication, security checks, and request handling.

---
## Course Context

**Course**: Back-End Development with .NET  
**Section**: Middleware Pipeline and Request Processing

This project explores how middleware components control the flow of HTTP requests in ASP.NET Core. It demonstrates how built-in and custom middleware interact in a pipeline, how ordering affects behaviour, and how middleware can enforce security, validation, and logging across the entire application.

---
## Concepts Demonstrated

- ASP.NET Core middleware pipeline architecture
- Built-in middleware (authentication, authorization, HTTP logging)
- Custom middleware components
- Conditional middleware execution with `UseWhen`
- Request validation and input sanitisation
- Simulated authentication and security checks
- Asynchronous middleware processing
- API key protection for write operations
- CRUD API implementation alongside middleware pipeline

---
## Project Structure

- **Models** - Workout session data model used by the API  
- **Contracts** – DTOs defining request structures for create/update operations  
- **Middleware** – Custom middleware handling validation, security checks, logging, and response behaviour  
- **Policies** – Request validation and security policy logic used by middleware  
- **Program.cs** – Application setup, middleware pipeline configuration, and API endpoint definitions  
- **Requests.http** - HTTP requests to provided endpoints for testing