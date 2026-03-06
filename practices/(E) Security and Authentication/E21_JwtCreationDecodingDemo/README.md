# (E21) JwtCreationDecodingDemo

A .NET console application demonstrating how **JSON Web Tokens (JWTs)** are created, signed, validated, and decoded. The project shows how tokens carry identity and role information used for authentication and authorisation.

---
## Course Context

**Course:** Security and Authentication  
**Section:** Introduction to JSON Web Tokens for RBAC

This project introduces **JWT-based authentication**, a stateless approach widely used in APIs and modern web applications. It demonstrates how tokens can securely transmit user identity and roles, enabling scalable authorisation without maintaining server-side sessions.

---
## Concepts Demonstrated

- JSON Web Token (JWT) structure and usage
- Stateless authentication with JWT
- Token creation and signing with **HMAC SHA-256**
- Token validation and signature verification
- Role-based claims embedded in tokens
- Decoding JWT claims after validation
- Secure token configuration using issuer, audience, and secret keys

---
## Project Structure

- **Models** – Token configuration, subject data, and validation result models  
- **Services** – JWT creation and validation logic  
- **Presentation** – Console output helpers for displaying tokens and claims  
- **Program.cs** – Entry point demonstrating token generation and validation