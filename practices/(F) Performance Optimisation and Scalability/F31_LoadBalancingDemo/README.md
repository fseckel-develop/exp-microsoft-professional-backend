# (F31) LoadBalancingDemo

A multi-project demo showing how load balancing distributes traffic across multiple backend instances using a gateway with YARP (Yet Another Reverse Proxy). The project demonstrates how scalable systems route requests to multiple servers while maintaining responsiveness and availability.

---
## Course Context

**Course:** Performance Optimisation and Scalability  
**Section:** Load Balancing and Traffic Distribution

This project demonstrates how a gateway can distribute requests across multiple backend API instances using **round-robin load balancing**, simulating a scalable architecture where multiple servers handle incoming traffic.

---
## Concepts Demonstrated

- Load balancing with a reverse proxy
- Round-robin request distribution
- Gateway pattern for routing traffic
- Running multiple backend service instances
- Basic health and diagnostics endpoints
- Stateless service design for scalability

---
## Project Structure

- **CatalogApi/**
    - **Controllers** – API endpoints for products, diagnostics, and system information  
    - **Infrastructure** – Instance metadata and in-memory product catalog  
    - **Models** – Product domain model  
    - **Program.cs** – Application entry point
    - **Requests.http** - HTTP requests for two API instances for testing

- **Gateway/**
    - **Controllers** – Gateway endpoints exposing proxy and routing information  
    - **Infrastructure** – Reverse proxy route configuration helpers  
    - **Program.cs** – Gateway startup and YARP configuration  
    - **Requests.http** - HTTP requests for observing load balancing directly