# (C42) OpenApiSwaggerDemo

An ASP.NET Core API demonstrating OpenAPI documentation and Swagger UI for both minimal API and controller-based endpoints.

---
## Course Context

**Course**: Back-End Development with .NET  
**Section**: OpenAPI and Swagger

This project demonstrates how ASP.NET Core APIs can be documented using the OpenAPI specification and exposed through Swagger UI. It shows how endpoint metadata, summaries, descriptions, and response definitions improve discoverability and make APIs easier to test and understand.

---
## Concepts Demonstrated

- OpenAPI-based API documentation
- Swagger UI integration in ASP.NET Core
- Swashbuckle configuration
- Documenting minimal API endpoints
- Documenting controller-based endpoints
- Endpoint summaries, descriptions, and response metadata
- CRUD operations with self-describing API contracts

---
## Project Structure

- **Models** – Recipe and cookbook data models used by the API  
- **Contracts** – DTOs defining request structures for create/update operations  
- **Controllers** – Controller-based cookbook endpoints with Swagger annotations  
- **Program.cs** – Swagger configuration, minimal API endpoints, and OpenAPI metadata setup  
- **Requests.http** - HTTP requests to provided endpoints for testing