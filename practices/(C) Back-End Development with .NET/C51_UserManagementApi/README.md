# (C51) UserManagementApi

An ASP.NET Core Web API demonstrating CRUD-based user management with authentication, logging, error handling middleware, and ultimately AI-assisted development using GitHub Copilot.

---
## Course Context

**Course**: Back-End Development with .NET  
**Section**: AI-Assisted API Development

This project demonstrates how GitHub Copilot can assist with scaffolding APIs, refactoring data structures, generating middleware, and debugging issues. It shows how AI-generated code can accelerate development while still requiring careful review and validation.

---
## Concepts Demonstrated

- CRUD API design with ASP.NET Core controllers
- Dependency injection with service-based architecture
- Middleware for authentication, logging, and error handling
- In-memory data storage using `ConcurrentDictionary`
- Input validation (email validation)
- Logging with ASP.NET Core logging providers
- AI-assisted code generation and refactoring with GitHub Copilot

---
## Project Structure

- **Models** – User domain model  
- **Contracts** – DTOs for creating and updating users  
- **Services** – User management service handling storage and operations  
- **Controllers** – API endpoints for user CRUD operations  
- **Middleware** – Authentication, logging, and global error handling middleware  
- **Validation** – Utility for validating email formats  
- **Program.cs** – Application setup, dependency injection, and middleware pipeline configuration