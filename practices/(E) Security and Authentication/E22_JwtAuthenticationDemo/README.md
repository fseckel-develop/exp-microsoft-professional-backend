# (E22) JwtAuthenticationDemo

An ASP.NET Core Web API demonstrating **JWT-based authentication and authorization**. The application issues JSON Web Tokens after successful login and protects API endpoints by validating tokens on incoming requests.

---
## Course Context

**Course:** Security and Authentication  
**Section:** JWT-Based API Security

This project demonstrates how APIs can be secured using **JSON Web Tokens (JWTs)**. It shows how authentication tokens are generated after login and how middleware validates those tokens before allowing access to protected endpoints.

---
## Concepts Demonstrated

- JSON Web Token (JWT) authentication
- Stateless API authentication using bearer tokens
- Token validation using ASP.NET Core middleware
- Securing endpoints with `[Authorize]`
- Role and claim support in JWT payloads
- Dependency injection for authentication services
- Simple in-memory user authentication for demonstration

---
## Project Structure

- **Controllers** – API endpoints for authentication and protected resources  
- **Contracts** – Data transfer objects for login requests and responses  
- **Services** – Token generation and user credential validation logic  
- **Infrastructure** – JWT configuration and authentication service registration  
- **Program.cs** – Application startup and authentication middleware configuration  
- **Requests.http** - HTTP requests to provided endpoints for testing