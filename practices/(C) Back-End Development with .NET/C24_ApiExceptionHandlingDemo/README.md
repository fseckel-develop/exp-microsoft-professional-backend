# (C24) ApiExceptionHandlingDemo

An ASP.NET Core Web API demonstrating global exception handling and structured error responses using custom middleware and application logging.

---
## Course Context

**Course**: Back-End Development with .NET  
**Section**: Error Handling and Logging

This project demonstrates how ASP.NET Core applications can handle errors consistently using global exception middleware. Instead of handling exceptions individually inside each controller, errors are intercepted centrally, logged, and returned as structured API responses.

---
## Concepts Demonstrated

- Global exception handling with custom middleware
- Structured error responses for APIs
- Custom exception types for validation and processing errors
- Logging with ASP.NET Core logging providers
- Consistent HTTP status codes for API errors
- Separation of error handling from business logic

---
## Project Structure

- **Controllers** – API endpoints performing text analysis logic  
- **Exceptions** – Custom exception types for validation and processing failures  
- **Middleware** – Global exception middleware and pipeline registration  
- **Models** – Structured error response model returned by the API  
- **Program.cs** – Application setup, logging configuration, and middleware registration  
- **Requests.http** - HTTP requests to provided endpoints for testing