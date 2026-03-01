# (B41) AsyncAwaitDemo

A console application demonstrating asynchronous programming in C# using `async` and `await`, including concurrent operations, background processing, and error handling.

---
## Course Context

**Course**: Introduction to Programming with C#  
**Section**: Asynchronous Programming

This project demonstrates how asynchronous methods allow applications to perform long-running or I/O-like operations without blocking the main program flow. Multiple tasks such as file imports, dataset processing, and API-style data loading are executed concurrently to improve responsiveness and efficiency.

---
## Concepts Demonstrated

- Asynchronous programming with `async` / `await`
- Non-blocking task execution
- Running concurrent operations with `Task.WhenAll`
- Simulating I/O-bound operations with `Task.Delay`
- Asynchronous error handling using `try-catch`
- Parallel batch processing
- Separation of concerns (Models / Services / Presentation)

---
## Project Structure

- **Models** – Data models representing products, reviews, and dashboard data  
- **Services** – Asynchronous services simulating file imports, analytics processing, background jobs, and data fetching  
- **Presentation** – Console output utilities for displaying results and progress  
- **Program.cs** – Demonstrates asynchronous workflows and concurrent task execution