# (E23) JwtBestPracticesDemo

An ASP.NET Core Web API demonstrating **JWT authentication best practices**, including short-lived access tokens, refresh tokens, role-based authorization, and secure token management patterns.

---
## Course Context

**Course:** Security and Authentication  
**Section:** JWT Security and Best Practices

This project expands on basic JWT authentication by introducing **token lifecycle management, refresh tokens, and role-based access control**. It demonstrates how production-ready JWT systems manage token expiration, rotation, and secure configuration.

---
## Concepts Demonstrated

- Short-lived JWT access tokens
- Refresh token workflow and token rotation
- Secure token validation using middleware
- Role-based authorization with `[Authorize(Roles = "...")]`
- Environment-based configuration for JWT secrets
- Separation of authentication infrastructure and application services
- In-memory user directory and refresh token store for demonstration

---
## Project Structure

- **Controllers** – API endpoints for login, token refresh, and protected resources  
- **Contracts** – Request and response DTOs used by the authentication API  
- **Services** – JWT issuance, refresh token storage, and user validation logic  
- **Infrastructure** – JWT configuration and authentication service setup  
- **Program.cs** – Application startup and authentication middleware configuration  
- **Requests.http** - HTTP requests to provided endpoints for testing