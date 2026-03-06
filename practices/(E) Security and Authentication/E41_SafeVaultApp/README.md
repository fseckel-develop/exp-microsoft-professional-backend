# (E41) SafeVaultApp

A secure ASP.NET Core Web API for user registration, login, JWT-based authentication, role-based authorisation, password hashing, and MySQL-backed user storage, with supporting automated tests.

## Course Context

**Course**: Security and Authentication 
**Section**: AI-Assisted Development of Security Principles

This project demonstrates how to apply secure coding practices in a real API by combining validated input, password hashing, JWT authentication, role-protected endpoints, parameterised database access, and test coverage.

---
## Key Concepts Demonstrated

- Secure coding principles
- Input validation with data annotations
- Password hashing with BCrypt
- JWT token generation and validation
- Authentication and role-based authorisation
- Parameterised SQL queries to reduce injection risk
- MySQL persistence for users
- Swagger support for secured API testing
- Automated unit and controller tests

---
## Project Structure

- **src/**
	- **SafeVault.Web/**
		- **Configurations** – Strongly typed settings for database and JWT options
		- **Contracts** – Request and response DTOs for authentication endpoints
		- **Controllers** – Auth and admin API endpoints
		- **Data** – User repository abstraction and MySQL repository implementation
		- **Infrastructure** – Database startup initialization logic
		- **Models** – Domain model for users
		- **Properties** – Launch settings
		- **Services** – Authentication, hashing, and JWT services
		- **wwwroot** – Static frontend demo file
		- **Program.cs** – App startup, DI, auth, Swagger, and middleware configuration

- **tests/**
	- **SafeVault.Web.Tests/**
		- **Controllers** – Controller and authorisation-related tests
		- **Services** – Tests for auth, password hashing, and JWT services

---
## How the Application Works

1. A user registers through the authentication API.
2. Input is validated before processing.
3. The password is hashed with BCrypt before storage.
4. The user is saved in MySQL through the repository layer.
5. On login, the submitted password is verified against the stored hash.
6. If valid, the API issues a JWT containing identity and role claims.
7. Protected endpoints use JWT authentication and role-based authorisation.
8. Admin-only routes are accessible only to users with the `Admin` role.

---
## Security Features

- **Input validation** using data annotations to prevent malformed input.
- **BCrypt password hashing** ensures credentials are never stored in plain text.
- **JWT authentication** provides stateless API security with signed tokens.
- **Role-based authorisation** restricts admin endpoints using `[Authorize(Roles = "Admin")]`.
- **Parameterized SQL queries** reduce SQL injection risks.
- **HTTPS redirection and middleware ordering** enforce secure request handling.

---
## Real-World Relevance

The architecture reflects common backend patterns used in production systems, including:

- Authentication and identity APIs
- Secure credential storage
- Token-based API protection
- Role-controlled administration endpoints
- Database-backed identity management