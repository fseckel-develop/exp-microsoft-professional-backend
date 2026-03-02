# (D11) EfCoreSQLiteDemo

A console-based .NET application demonstrating how to use **Entity Framework Core with SQLite** to model data, seed a database, and perform basic CRUD operations using LINQ.

---
## Course Context

**Course:** Database Integration and Management  
**Section:** Entity Framework Core & Data Persistence

This project introduces **Object–Relational Mapping (ORM)** using Entity Framework Core. It shows how C# objects can be mapped to relational database tables and how CRUD operations can be performed without writing raw SQL.

---
## Concepts Demonstrated

- Object–Relational Mapping (ORM) with **Entity Framework Core**
- SQLite database integration
- Data modelling with **entities and relationships**
- Database configuration using **Fluent API**
- **DbContext** for database interaction
- CRUD operations using **LINQ**
- Database seeding with initial data
- Separation of commands and queries (basic CQRS-style structure)

---
## Project Structure

- **Models** – Entity classes representing database tables (`Book`, `Category`)  
- **Data** – EF Core database context, entity configurations, and seed data  
- **Services**
  - `LibraryQueries` – Read operations using LINQ
  - `LibraryCommands` – Create operations for books  
- **Program.cs** – Application entry point demonstrating database interaction