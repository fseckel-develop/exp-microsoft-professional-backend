# (C32) SecureJsonDemo

A console application demonstrating secure JSON serialisation practices, including encryption, integrity verification, and safe deserialisation of structured data.

---
## Course Context

**Course**: Back-End Development with .NET  
**Section**: Secure Serialisation

This project demonstrates how serialised data can be protected against tampering, exposure, and unsafe deserialisation. It applies encryption and integrity checks to ensure that sensitive information remains confidential and that incoming data has not been modified.

---
## Concepts Demonstrated

- Secure JSON serialisation and deserialisation
- Input validation before serialisation
- Encryption of sensitive data using AES
- Data integrity verification using HMAC-SHA256
- Safe deserialisation with trusted-source checks
- Protection against tampering and malformed payloads
- Separation of concerns (Models / Services / Presentation)

---
## Project Structure

- **Models** – Document package model containing metadata and encrypted contents  
- **Services** – Secure packaging service handling encryption, hashing, and serialisation logic  
- **Presentation** – Console output utilities for displaying results  
- **Program.cs** – Demonstrates secure packaging, restoration, and validation checks