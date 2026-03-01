# (C43) ClientGenerationDemo

A console application demonstrating how OpenAPI/Swagger definitions can be used to generate strongly typed API clients using NSwag.

---
## Course Context

**Course**: Back-End Development with .NET  
**Section**: API Client Generation

This project demonstrates how API clients can be automatically generated from an OpenAPI (Swagger) specification. It compares manual HTTP client implementation with a generated client that provides strongly typed methods for interacting with API endpoints.

---
## Concepts Demonstrated

- OpenAPI-based client generation
- Using **NSwag** to generate C# API clients
- Consuming APIs with `HttpClient`
- Strongly typed API client usage
- Automatic request/response serialization
- Client configuration and code generation automation
- Comparing manual API consumption vs generated SDK-style clients

---
## Project Structure

- **Configuration** – Client generation configuration and API settings  
- **Services** – Manual HTTP client and Swagger-based client generator  
- **Generated** – Auto-generated API client code produced from the OpenAPI specification  
- **Demos** – Demonstrations of manual API usage and generated client usage  
- **Models** – Recipe data model used by the demo clients  
- **Presentation** – Console output utilities for displaying results  
- **Program.cs** – Orchestrates client generation and demonstrates API consumption