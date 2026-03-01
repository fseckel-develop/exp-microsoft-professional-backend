# (C23) ApiDependencyInjectionDemo

An ASP.NET Core Web API demonstrating dependency injection, service lifetimes, and how services are resolved across middleware and endpoints.

---
## Course Context

**Course**: Back-End Development with .NET  
**Section**: Dependency Injection

This project demonstrates how ASP.NET Core’s built-in dependency injection container manages service creation and delivery. It shows how services can be registered with different lifetimes and injected into middleware and API endpoints to observe their behaviour across the request pipeline.

---
## Concepts Demonstrated

- Dependency Injection in ASP.NET Core
- Loose coupling through interfaces and abstractions
- Service registration using `AddSingleton`, `AddScoped`, and `AddTransient`
- Injecting services into endpoints and middleware
- Understanding service lifetimes
- Middleware interaction with DI services
- Observing service instance behaviour across requests

---
## Project Structure

- **Services** – Interface and implementation for request auditing and instance tracking  
- **Middleware** – Custom middleware resolving services from the DI container  
- **Program.cs** – Service registration, middleware pipeline configuration, and API endpoints  
- **Requests.http** - HTTP requests to provided endpoints for testing