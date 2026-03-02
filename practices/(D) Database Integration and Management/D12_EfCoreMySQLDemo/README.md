# (D12) EfCoreMySQLDemo

A .NET console application demonstrating how to use **Entity Framework Core with MySQL** to perform CRUD operations using a relational database and EF Core migrations.

---
## Course Context

**Course:** Database Integration and Management  
**Section:** Entity Framework Core & Database Integration

This project extends the EF Core concepts by connecting a .NET application to a **MySQL database**. It demonstrates how EF Core manages database schema changes through **migrations** and enables CRUD operations using C# objects instead of raw SQL.

---
## Concepts Demonstrated

- Entity Framework Core with **MySQL**
- Database schema management using **EF Core Migrations**
- Configuring EF Core using **DbContextOptions**
- Managing database connections via **configuration files**
- CRUD operations using EF Core
- Service-based data access layer
- Design-time DbContext factory for migration support

---
## Project Structure

- **Models** – Entity class representing the database table (`Drink`)  
- **Data**
  - `CoffeeShopDbContext` – EF Core database context
  - `CoffeeShopDbContextFactory` – design-time factory for migrations
  - `Migrations` – generated database schema migrations  
- **Services** – `DrinkService` handling CRUD operations  
- **Program.cs** – Application entry point demonstrating database operations  
- **Configuration** – connection strings stored in configuration files  
- **initialize-database.ps1** – helper script to create the database and run migrations