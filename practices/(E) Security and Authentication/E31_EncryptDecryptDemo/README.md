# (E31) EncryptDecryptDemo

An ASP.NET Core minimal API demonstrating **AES-based symmetric encryption and decryption**. The application exposes endpoints for encrypting plaintext and decrypting ciphertext using a configured encryption key.

---
## Course Context

**Course:** Security and Authentication  
**Section:** Encryption and Data Security

This project demonstrates how sensitive data can be protected using **AES encryption**. It shows how plaintext can be securely transformed into ciphertext and later restored using the correct key, illustrating core principles of symmetric encryption.

---
## Concepts Demonstrated

- Symmetric encryption using AES
- Secure key handling via configuration
- Encryption and decryption workflows
- Base64 encoding for safe transport of encrypted data
- Error handling for invalid ciphertext or keys
- Minimal API endpoint design

---
## Project Structure

- **Contracts** – Request and response DTOs used by the encryption API  
- **Services** – AES encryption and decryption logic  
- **Infrastructure** – Encryption configuration and dependency registration  
- **Routing** – Minimal API endpoint definitions for encryption operations  
- **Program.cs** – Application startup and endpoint registration  
- **Requests.http** - HTTP requests to provided endpoints for testing