# (E11) ControlledRolesClaimsDemo

An ASP.NET Core MVC and API application demonstrating user authentication and controlled access to application resources using role-based and claim-based authorisation with ASP.NET Identity.

---
## Course Context

**Course:** Security and Authentication  
**Section:** Authentication & Authorisation with ASP.NET Identity

This project demonstrates how ASP.NET Identity can be used to implement secure user authentication and structured authorisation within an ASP.NET Core application. It shows how users can register and log in, how authentication is maintained using cookies, and how roles and claims can be used to control access to different parts of the system. Authorisation policies are then used to enforce these access rules across both MVC views and API endpoints.

---
## Concepts Demonstrated

- User registration and login using ASP.NET Identity
- Password hashing and secure credential storage
- Authentication using SignInManager and UserManager
- Cookie-based authentication for maintaining login sessions
- Role-based authorisation
- Claim-based authorisation
- Authorisation policies in ASP.NET Core
- Protected MVC views and secured API endpoints
- Identity bootstrapping and demo user seeding
- API-friendly authentication responses for HTTP clients

---
## Project Structure

- **Controllers** – MVC and API controllers managing AuthN, endpoints, and role administration  
- **Models** – View models for login, registration, and error handling  
- **Data** – Identity database context responsible for storing users, roles, and claims  
- **Infrastructure**
  - Identity configuration and service registration
  - Authorisation policy definitions
  - Cookie authentication configuration
  - Identity bootstrap service for demo users and roles  
- **Views** – Razor views for account management and role administration  
- **Program.cs** – Application startup configuration, AuthN/AuthZ middleware, and route mapping  
- **Requests.http** – HTTP requests used to test authentication flows and protected endpoints