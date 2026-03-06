# (E32) FileEncryptionDemo

An ASP.NET Core Razor Pages application demonstrating **AES-based file encryption and decryption**. Users can upload files, process them securely, and download the encrypted or decrypted output.

---
## Course Context

**Course:** Security and Authentication  
**Section:** Encryption and Data Security

This project extends basic encryption concepts to file handling. It demonstrates how files can be encrypted before storage or transfer and later decrypted using the same symmetric key, showing a practical use of AES in a web application.

---
## Concepts Demonstrated

- Symmetric file encryption using AES
- Encrypting and decrypting uploaded files
- Secure key handling via configuration
- Razor Pages for file upload workflows
- Safe file-name handling for uploaded content
- Error handling for invalid encrypted files or wrong keys
- Serving processed files from a web application

---
## Project Structure

- **Infrastructure** – Encryption configuration and dependency registration  
- **Services** – AES-based file encryption and decryption logic  
- **Pages** – Razor Pages for upload, processing, and result display  
- **Data** – Sample input and output files used for the demo  
- **wwwroot** – Static assets plus uploaded and processed files  
- **Program.cs** – Application startup and middleware configuration