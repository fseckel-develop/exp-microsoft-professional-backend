# (G33) LoadBalancingAlgorithmsDemo

A console application demonstrating **common load balancing strategies** by routing simulated client requests across multiple backend instances. The project compares how different algorithms distribute traffic and affect backend usage.

---
## Course Context

**Course**: Data Structures and Algorithms  
**Section**: Load Balancing Algorithms

This project demonstrates how request-routing strategies influence scalability and reliability in distributed systems. It compares simple, dynamic, weighted, and client-consistent routing approaches in a small gateway-style simulation.

---
## Concepts Demonstrated

- Load balancing fundamentals
- Round Robin routing
- Least Connections routing
- Weighted Round Robin routing
- IP Hashing for consistent client assignment
- Backend capacity weighting
- Request distribution analysis

---
## Project Structure

- **Algorithms** – Load balancing strategy implementations  
- **Models** – Backend, request, dataset, and routing result models  
- **Services** – Load balancer orchestration logic  
- **Scenarios** – Strategy comparison demo flow  
- **Presentation** – Console output helpers  
- **Program.cs** – Application entry point

---
## How the Application Works

1. A set of backend server instances is created with configurable capacity.
2. Client requests are generated to simulate traffic.
3. Different routing strategies distribute requests across the servers.
4. Each algorithm is executed against the same dataset.
5. The system tracks how requests are distributed across instances.
6. Console output shows the impact of each strategy on backend load.