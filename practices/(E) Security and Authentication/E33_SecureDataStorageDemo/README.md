# (E33) SecureDataStorageDemo

An ASP.NET Core minimal API demonstrating **secure data storage practices** by combining encryption, access control, and authenticated API access. Messages are encrypted before storage and can only be accessed by their authenticated owners.

---
## Course Context

**Course:** Security and Authentication  
**Section:** Secure Data Storage

This project demonstrates how sensitive information can be stored securely using **AES encryption and authenticated access with JWTs**. It illustrates how encrypted data can be stored in a database and decrypted only for authorised users.

---
## Concepts Demonstrated

- Encrypting sensitive data before database storage
- AES-based message encryption and decryption
- Protecting APIs with JWT authentication
- Access control using user identity from JWT claims
- Secure configuration for encryption and signing keys
- Minimal API endpoints for secure data access
- Separation of encryption, authentication, and persistence layers

---
## Project Structure

- **Contracts** – Request and response DTOs used by the API  
- **Models** – Database entities representing stored messages  
- **Services** – Encryption logic for protecting message content  
- **Infrastructure** – Configuration and service registration for JWT and encryption  
- **Data** – Database context for storing encrypted messages  
- **Routing** – Minimal API endpoints for authentication and message handling  
- **Program.cs** – Application startup and service configuration  
- **Requests.http** - HTTP requests to provided endpoints for testing