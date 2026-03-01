# (B32) DesignPatternsDemo

A console application demonstrating several classic software design patterns through a simple monitoring system that distributes station readings to different display types.

---
## Course Context

**Course**: Introduction to Programming with C#  
**Section**: Design Patterns

This project demonstrates how common design patterns can be applied to structure software systems in a flexible and maintainable way. A singleton monitoring hub distributes environmental data to various observers while different station sources are unified through adapters.

---
## Concepts Demonstrated

- Singleton pattern for shared system state
- Factory pattern for flexible object creation
- Observer pattern for event-based updates
- Adapter pattern for integrating incompatible interfaces
- Interface-based architecture
- Loose coupling between system components

---
## Project Structure

- **Models** – Monitoring system components such as the central hub and station sources  
- **Observer** – Display observers that react to monitoring updates  
- **Factory** – Factory responsible for creating display instances  
- **Adapter** – Adapter layer enabling different station sources to share a common interface  
- **Program.cs** – Demonstrates how the patterns interact within the application