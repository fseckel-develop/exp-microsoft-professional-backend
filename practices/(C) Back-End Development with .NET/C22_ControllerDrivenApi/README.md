# (C22) ControllerDrivenApi

An ASP.NET Core Web API demonstrating controller-based routing and CRUD operations using a simple in-memory product catalog.

---
## Course Context

**Course**: Back-End Development with .NET  
**Section**: Building Web APIs and CRUD Operations

This project extends the minimal API approach by introducing controller-based routing using ASP.NET Core MVC. It demonstrates how controllers organise API endpoints, how route attributes define URL mappings, and how CRUD operations can be structured using action methods.

---
## Concepts Demonstrated

- Controller-based API design in ASP.NET Core
- Attribute routing with `[Route]`, `[HttpGet]`, `[HttpPost]`, `[HttpPut]`, `[HttpDelete]`
- CRUD operations for resource management
- Query parameter filtering
- Route parameters and constraints
- Data transfer objects (DTOs) for request handling
- Structured API responses using `ActionResult`

---
## Project Structure

- **Models** – Product data model used by the API  
- **Contracts** – DTOs defining request structures for create/update operations  
- **Controllers** – Controller class implementing CRUD endpoints  
- **Program.cs** – ASP.NET Core application setup and controller routing configuration  
- **Requests.http** - HTTP requests to provided endpoints for testing