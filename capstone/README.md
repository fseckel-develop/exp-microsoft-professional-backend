LogiTrack

Secure Order & Inventory Management API

LogiTrack is a secure backend API built with ASP.NET Core Web API for managing warehouse inventory and customer orders. The project focuses on clean architecture, strong domain modelling, role-based access control, and performance optimisation.

⸻

🔧 Tech Stack
	•	ASP.NET Core Web API
	•	Entity Framework Core (SQLite)
	•	ASP.NET Core Identity
	•	JWT Bearer Authentication
	•	IMemoryCache
	•	xUnit & FluentAssertions

⸻

🚀 Key Features
	•	Full CRUD operations for Inventory and Orders
	•	Many-to-many relationship modelled via OrderItem junction entity
	•	Encapsulated business logic inside domain models
	•	JWT authentication with role-based authorisation
	•	Policy-based access control (Admin, WarehouseStaff, SalesStaff)
	•	In-memory caching with automatic invalidation
	•	Query optimisation using AsNoTracking() and projection
	•	Comprehensive unit testing

⸻

🔐 Security
	•	JWT validation (issuer, audience, lifetime, signing key)
	•	Strong password policies and account lockout
	•	Email confirmation required
	•	Role-restricted destructive actions
	•	DTO-based request/response protection

⸻

⚡ Performance
	•	IMemoryCache for frequently accessed endpoints
	•	Sliding expiration with mutation-based invalidation
	•	Eager loading to prevent N+1 queries
	•	Reduced database tracking overhead

⸻

🎯 What This Project Demonstrates
	•	Secure REST API design
	•	Proper many-to-many relationship modelling
	•	Clean separation of concerns
	•	Policy-based RBAC implementation
	•	Production-style backend architecture

⸻

This project showcases backend engineering practices aligned with real-world enterprise API development.