## Objective

- Plan API endpoint security using JWTs
- Identify:
	- Which routes require JWT validation
	- Required roles or claims for access
	- Reasons for applying restrictions

---
## Core Security Concepts

- JWT validation:
	- Ensures only authenticated users access protected routes
- Role-based access control (RBAC):
	- Limits actions based on user roles
- Claims-based authorisation:
	- Restricts access to specific resources (e.g., user ID, patient ID)

---
## Key Takeaways

- JWT validation is essential for protecting sensitive API routes
- Roles limit what actions users can perform
- Claims enforce fine-grained, resource-level access
- Combining JWTs, roles, and claims creates secure and scalable APIs


---
---
## Example 1: Medical Records System

### Scenario Overview

- API manages sensitive patient data
- Routes involved:
	- GET /patients/{id}
	- PUT /patients/{id}
	- GET /reports

---
### Route Security Planning

- GET /patients/{id}
	- JWT validation required
	- Roles:
		- doctor
		- nurse
	- Claims:
		- Must include patient ID
- PUT /patients/{id}
	- JWT validation required
	- Roles:
		- doctor
	- Claims:
		- Must include patient ID
- GET /reports
	- JWT validation required
	- Roles:
		- manager
		- admin
	- Claims:
		- None

---
### Rationale for Restrictions

- Patient data is highly sensitive and must be protected
- Role separation prevents unauthorized actions:
	- Nurses can view but not modify records
	- Doctors can update patient information
- Claims ensure users only access records they are authorized to view or modify



---
---
## Example 2: E-Commerce Platform

### Scenario Overview

- API supports browsing products and managing orders
- Routes involved:
	- GET /products
	- POST /orders
	- GET /orders/{id}

---
### Route Security Planning

- GET /products
	- JWT validation not required
	- Public access
- POST /orders
	- JWT validation required
	- Roles:
		- customer
	- Claims:
		- Must include user ID
- GET /orders/{id}
	- JWT validation required
	- Roles:
		- customer
	- Claims:
		- Must include order ID

---
### Rationale for Restrictions

- Public product listings improve usability
- Authentication prevents fraudulent order creation
- Claims ensure users can only view their own orders



---
---
## Example 3: Social Media Platform

### Routes to Secure

- GET /users/{id}
- POST /posts
- DELETE /posts/{id}

---
### Security Plan Summary

- GET /users/{id}
	- JWT validation required
	- Roles:
		- user
	- Claims:
		- Must include user ID
- POST /posts
	- JWT validation required
	- Roles:
		- user
	- Claims:
		- Must include user ID
- DELETE /posts/{id}
	- JWT validation required
	- Roles:
		- user
	- Claims:
		- Must include user ID and post ID



---
---
## Example 4: Learning Management System (LMS)

### Routes to Secure

- GET /courses
- POST /assignments
- GET /grades/{id}

---
### Security Plan Summary

- GET /courses
	- JWT validation not required
	- Public access
- POST /assignments
	- JWT validation required
	- Roles:
		- instructor
	- Claims:
		- Must include course ID
- GET /grades/{id}
	- JWT validation required
	- Roles:
		- student
		- instructor
	- Claims:
		- Students: must include student ID
		- Instructors: must include course ID