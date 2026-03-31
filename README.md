# 🚀 Microsoft Professional Back-End Certification

![Microsoft Certification](https://img.shields.io/badge/Microsoft-Back--End%20Developer%20Certificate-0078D4)
![C#](https://img.shields.io/badge/Language-C%23-512BD4)
![ASP.NET Core](https://img.shields.io/badge/Framework-ASP.NET%20Core-512BD4)
![EF Core](https://img.shields.io/badge/ORM-EF%20Core-512BD4)
![OpenAPI](https://img.shields.io/badge/API%20Docs-OpenAPI%20%2F%20Swagger-6BA539)  
![Database](https://img.shields.io/badge/Database-SQL%20Server%20%7C%20SQLite-F29111)
![Redis](https://img.shields.io/badge/Cache-Redis-DC382D)
![Authentication](https://img.shields.io/badge/Security-JWT%20%2B%20RBAC-B22222)
![Testing](https://img.shields.io/badge/Testing-xUnit%20%7C%20Moq-green)
![CI/CD](https://img.shields.io/badge/CI/CD-GitHub%20Actions%20%2B%20Azure%20DevOps-0078D4)

This repository consolidates applied backend engineering work completed throughout the [**Back-End Developer Professional Certificate**](https://www.coursera.org/professional-certificates/microsoft-back-end-developer) program delivered by Microsoft on Coursera. The focus extends beyond coursework — integrating layered API design, authentication systems, data access optimisation, and CI/CD workflows into a cohesive, production-style backend system.  

In here you will find:
- 🏛 **Capstone Project** – Production-style logistics API – For full documentation see [**`capstone/README.md`**](capstone/README.md).
- ⭐ **Featured Projects** – Curated algorithm, system design, and DevOps demonstrations.
- 🧩 **Practice Projects** – Focused implementations of backend concepts.
- 📚 **Explanatory Notes** – Technical breakdowns for each course module.
- 📈 **Living Portfolio** – Continuous improvements and refinements.

---
## 🎯 Engineering Focus

- Clean architecture & layered API design
- Secure authentication & authorisation systems
- Performance-aware backend development
- Scalable system design fundamentals
- DevOps-driven deployment workflows

---
## 🧠 Architectural Principles Applied

- Separation of concerns & layered design
- Dependency inversion & constructor-based injection
- Stateless REST API architecture
- Idempotent endpoint design principles
- Structured exception handling & middleware pipelines
- Performance-aware data access patterns

---
## 🧰 Technology Stack

- C#
- .NET / ASP.NET Core
- Entity Framework Core
- SQL Server + SQLite
- Redis for Distributed Caching
- YARP for Load Balancing
- JWT / Bearer Authentication
- ASP.NET Core Identity
- Swagger / OpenAPI
- xUnit + FluentAssertions + Moq
- Git & GitHub
- GitHub Actions
- Azure CLI
- Azure DevOps

---
## 🗃️ Repository Structure

```text
.
├── notes/          → Structured theory notes (per course & module)
├── practices/      → Hands-on exercises & focused demos
├── capstone/       → LogiTrack production-style API project
├── certificates/   → Completion certificates
└── README.md
```

This structure ensures full traceability from:  
Theory → Isolated Practice → Integrated System Design  

---
## 🎓 Program Overview

The specialisation consists of 8 courses covering modern backend engineering with C#, .NET, databases, security, performance, and DevOps:

#### 1. Course: [Foundations of Coding Back-End](https://www.coursera.org/learn/foundations-of-coding-back-end?specialization=microsoft-back-end-developer)  

- Algorithm basics & logical problem-solving
- Programming fundamentals (control structures, functions)
- Git, project planning, and development workflows

#### 2. Course: [Introduction to Programming with C#](https://www.coursera.org/learn/introduction-to-programming-with-c-sharp?specialization=microsoft-back-end-developer)

- Object-oriented programming in C#
- Asynchronous programming with `async`/`await`
- Project architecture in .NET

#### 3. Course: [Back-End Development with .NET](https://www.coursera.org/learn/back-end-development-with-dotnet?specialization=microsoft-back-end-developer)

- Building RESTful APIs with ASP.NET Core
- Middleware, dependency injection, and routing
- Serialisation, error handling, and OpenAPI (Swagger) integration

#### 4. Course: [Database Integration and Management](https://www.coursera.org/learn/database-integration-and-management?specialization=microsoft-back-end-developer)

- Entity Framework Core and ORM fundamentals
- SQL querying, joins, and data manipulation
- Transactions, performance tuning, and database security

#### 5. Course: [Security and Authentication](https://www.coursera.org/learn/security-and-authentication?specialization=microsoft-back-end-developer)

- ASP.NET Identity and user management
- JWT-based authentication and RBAC
- Data encryption and secure storage practices

#### 6. Course: [Performance Optimisation and Scalability](https://www.coursera.org/learn/performance-optimization-and-scalability?specialization=microsoft-back-end-developer)

- In-memory and distributed caching (Redis)
- Query optimisation and indexing strategies
- Scalable architecture patterns and load balancing

#### 7. Course: [Data Structures and Algorithms](https://www.coursera.org/learn/msft-data-structures-and-algorithms?specialization=microsoft-back-end-developer)

- Linear data structures and complexity analysis (Big O)
- Sorting, searching, trees and graph algorithms
- Trade-offs between dynamic programming vs. greedy algorithms 

#### 8. Course: [Deployment and DevOps](https://www.coursera.org/learn/deployment-and-devops?specialization=microsoft-back-end-developer)

- Deploying applications to Azure
- CI/CD pipelines with GitHub Actions and Azure DevOps
- Monitoring, logging, and automated deployment scripts

---
## 🏛 Capstone Project

The capstone project (`capstone/`) is a structured backend system simulating a logistics and inventory management API called **LogiTrack**. For more detailed documentation please check out the [**`capstone/README.md`**](capstone/README.md).

#### Run the Project:

```shell
### from repository root
cd capstone/src/LogiTrack.Api
dotnet run
```  

#### Architecture Overview:

```text
LogiTrack/
├── LogiTrack.Api/
│   ├── Contracts          → DTOs for API requests and responses
│   ├── Controllers        → REST API endpoints
│   ├── Data               → Entity Framework DbContext and database access
│   ├── Models             → Domain entities and data models
│   ├── Requests           → Request models used for input validation
│   ├── Services           → Business logic and application workflows
│   └── Program.cs         → Application startup and middleware configuration
└── LogiTrack.Api.Tests/
    ├── Controllers        → API endpoint tests
    ├── Data               → Repository and data access tests
    ├── Models             → Model validation tests
    └── Services           → Business logic tests
```

#### Implemented Concepts:

  - Clean Controller-Service separation
  - DTO pattern
  - Dependency Injection
  - Entity Framework Core integration
  - CRUD operations
  - Unit testing
  - Role-Based Access Control
  - JWT authentication
  - Performance optimisation with caching
  - Structured middleware configuration
  - API documentation via Swagger/OpenAPI
  
This project reflects real-world layering, maintainability principles, and backend best practices.  

---
## ⭐ Featured Projects

A curated selection of projects demonstrating key backend engineering concepts, algorithmic thinking, and DevOps automation.

- **(E41) SafeVaultApp** [**`(➤)`**](practices/(E)%20Security%20and%20Authentication/E41_SafeVaultApp)  
  Secure ASP.NET Core API demonstrating JWT authentication, role-based access control, password hashing, and MySQL-backed user storage.

- **(G11) LinearDataSupportTicketApi** [**`(➤)`**](practices/(G)%20Data%20Structures%20and%20Algorithms/G11_LinearDataSupportTicketApi)  
  ASP.NET Core Web API demonstrating arrays, queues, stacks, and linked lists through a support ticket workflow.

- **(G32) GraphCampusNavigator** [**`(➤)`**](practices/(G)%20Data%20Structures%20and%20Algorithms/G32_GraphCampusNavigator)  
  Graph traversal and shortest-path algorithms (DFS, BFS, Dijkstra, A*) in a campus navigation scenario.

- **(G33) LoadBalancingAlgorithmsDemo** [**`(➤)`**](practices/(G)%20Data%20Structures%20and%20Algorithms/G33_LoadBalancingAlgorithmsDemo)  
  Simulation of request routing strategies including Round Robin, Least Connections, Weighted Round Robin, and IP Hashing.

- **(G43) GreedyVsDynamicScheduler** [**`(➤)`**](practices/(G)%20Data%20Structures%20and%20Algorithms/G43_GreedyVsDynamicScheduler)  
  Comparison of greedy scheduling and dynamic programming for weighted interval scheduling optimisation.

- **(H42) PipelineGenerationDemo** [**`(➤)`**](practices/(H)%20Deployment%20and%20DevOps/H42_PipelineGenerationDemo)  
  Demonstrates how AI tools can generate CI/CD pipelines for GitHub Actions and Azure DevOps.

---
## 🧩 Practice Projects

The `practices/` directory contains focused implementation projects demonstrating backend engineering concepts, algorithmic thinking, and DevOps workflows.
Each project mirrors a small real-world scenario and focuses on a specific backend technique or architectural concept. For detailed information about each project, see the **`README.md`** inside its directory.

#### Run Practice Projects (Example):

```shell
### from repository root
cd practices/C21_BasicApi
dotnet restore
dotnet run
```

#### Practice Projects include:

  - CRUD APIs with ASP.NET Core
  - EF Core database integration demos
  - JWT-secured APIs
  - Role-based authentication systems
  - In-memory and Redis caching demos
  - Load balancing simulation
  - Async processing pipelines
  - Algorithm implementations (Sorting, Searching, Graphs, Dynamic Programming)
  - CI/CD demos with GitHub Actions and Azure DevOps

Each practice project is isolated in its own solution file (`.sln`) to mirror real-world development environments.    

---
## 📚 Explanatory Notes

The `notes/` directory contains:
  - Module-by-module breakdowns
  - Architecture explanations
  - Diagrams & flowcharts
  - Reflections & best practices
  - Practical design considerations

#### Directory Layout:

```text
notes/
 ├── (A) Foundations of Coding Back-End
 ├── (B) Introduction to Programming with C-Sharp
 ├── (C) Back-End Development with .NET
 ├── (D) Database Integration and Management
 ├── (E) Security and Authentication
 ├── (F) Performance Optimisation and Scalability
 ├── (G) Data Structures and Algorithms
 ├── (H) Deployment and DevOps
 └── (X) Capstone Project
```

These notes document the technical concepts explored throughout the program and provide deeper explanations behind the implementations found in the practice projects and capstone.  

---
## 📌 Summary

This repository represents a backend engineering portfolio covering:
- Structured backend engineering training
- Applied architectural thinking
- Secure API implementation
- Performance-aware system design
- DevOps-integrated deployment workflows
  
It serves as a comprehensive backend engineering portfolio demonstrating production-ready development practices.

---
## 🙌 Get Involved

Feel free to:
- Clone or fork the repository  
- Explore the capstone and practice projects  
- Study the notes and implemented architectural principles  
- Adapt or extend the projects for your own learning

---
## 📝 License / Disclaimer

This repository contains **original work created by the author** for learning and portfolio purposes. It is intended for **educational and personal learning only**. All course materials and assignments from the Coursera program remain the property of Microsoft and Coursera.

---
### Thanks for Visiting!

I hope this repository serves as both a learning guide and a showcase of applied backend engineering skills.

Happy coding! 🚀
