# (E12) ExternalAuthDemo

A minimal ASP.NET Core application demonstrating **external authentication using Google OAuth 2.0**. The project shows how users can authenticate through a trusted third-party provider instead of creating local accounts.

---
## Course Context

**Course:** Security and Authentication  
**Section:** External Authentication Providers

This project introduces **external authentication** in ASP.NET Core. It demonstrates how applications can delegate identity verification to external providers such as Google, improving both security and user experience.

---
## Concepts Demonstrated

- External authentication using **Google OAuth 2.0**
- ASP.NET Core authentication middleware
- Securing API endpoints with authorization
- Configuration of authentication credentials through **appsettings**
- OAuth redirect flow between application and provider

---
## Project Structure

- **Program.cs** – Application configuration including authentication middleware and secure routes  
- **appsettings.Development.json** – Stores Google OAuth Client ID and Client Secret for development  