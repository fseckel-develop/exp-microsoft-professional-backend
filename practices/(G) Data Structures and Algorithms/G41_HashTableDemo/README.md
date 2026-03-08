# (G41) HashTableDemo

A console application that demonstrates how a custom hash table can store and retrieve API clients by API key, including updates, lookups, revocation, and bucket distribution.

---
## Course Context

**Course**: Data Structures and Algorithms  
**Section**: Hash Tables and Hashing

This project demonstrates how hashing enables fast key-based lookup and how collisions can be handled using chained buckets. It applies these ideas to a simple API key registry and request authentication flow.

---
## Concepts Demonstrated

- Custom hash table implementation
- Key-value storage
- Hash-based lookup
- Collision handling with chaining
- API key registration and revocation
- Request authentication using hashed lookup
- Bucket distribution inspection

---
## Project Structure

- **Collections** – Custom hash table implementation
- **Data** – Sample API clients and requests
- **Models** – Domain models for clients, requests, and authentication results
- **Services** – API key registry and request authentication logic
- **Presentation** – Console output helpers
- **Program.cs** – Application entry point