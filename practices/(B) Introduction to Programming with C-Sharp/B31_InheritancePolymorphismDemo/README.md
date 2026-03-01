# (B31) InheritancePolymorphismDemo

A console application demonstrating object-oriented design using inheritance and polymorphism through a simple notification system supporting multiple delivery channels.

---
## Course Context

**Course**: Introduction to Programming with C#  
**Section**: Inheritance and Polymorphism

This project demonstrates how base classes and derived classes can be used to reuse functionality and extend behaviour. A shared notification interface is implemented through a base class, while specific delivery mechanisms (Email, SMS, Push) override behaviour where necessary.

---
## Concepts Demonstrated

- Classes and object instantiation
- Encapsulation through access modifiers
- Inheritance for code reuse
- Polymorphism through method overriding
- Abstract base classes
- Template-style method design
- Extensible object-oriented architecture

---
## Project Structure

- **Models** – Data model representing a notification message  
- **Channels** – Base notification channel and specific implementations (Email, SMS, Push)  
- **Program.cs** – Application entry point demonstrating polymorphic channel processing