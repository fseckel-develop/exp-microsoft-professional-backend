## Objective

Evaluate different load balancing algorithms and choose the most suitable approach for various back-end system needs based on traffic patterns, server capacity, and session requirements.  

---
## Example 1: Load Balancing for an E-Commerce Website

**Scenario**:
-	High traffic spikes during promotions and flash sales  
-	All servers have **equal processing power**  
-	Traffic needs to be evenly distributed to **avoid overloading any single server**  

**Algorithm Choice**: Round-Robin  

**Justification**:
-	Round-Robin evenly distributes requests across all servers in a cycle  
-	Best for stateless requests where session persistence is not needed  
-	Ensures no single server gets overwhelmed during high traffic periods  

---
## Example 2: Load Balancing for Session-Based Application

**Scenario**:
-	Financial trading platform requiring **session persistence**  
-	Users must stay connected to **same server** for active sessions to ensure data consistency  

**Algorithm Choice**: IP Hashing

**Justification**:
-	IP Hashing consistently routes requests from the same IP address to the same server  
-	Preserves session persistence, preventing login disruptions  
-	Ensures a smooth and reliable user experience for session-sensitive applications  

---
## Example 3: Load Balancing for Video Streaming Service

**Scenario**:
-	Content delivery network (CDN) serving video across multiple regions  
-	Servers have **varying capacities**  
-	Goal: Avoid overloading weaker servers and underutilising stronger ones  

**Algorithm Choice**: Weighted Round-Robin

**Justification**:
-	Weighted Round-Robin distributes requests according to server capacity  
-	Powerful servers handle a larger share of traffic, optimising efficiency  
-	Prevents overload on weaker servers while maximising overall throughput  

---
## Example 4: Load Balancing for Gaming Servers

**Scenario**:
-	Multiplayer online game requiring **real-time connectivity and low latency**  
-	Players must **stay connected to the same server** during a match  

**Algorithm Choice**: IP Hashing

**Justification**:
-	IP Hashing ensures consistent server assignment for each player  
-	Prevents lag and disconnections critical for smooth gameplay  
-	Provides reliable, session-persistent connections during matches  

---
## Example 5: Load Balancing for Cloud-Based AI Processing

**Scenario**:
-	Cloud-based AI workloads with servers of **varying processing power**  
-	Some AI models require **high computing resources**, others run on lower-end servers  

**Algorithm Choice**: Weighted Round-Robin

**Justification**:
-	Weighted Round-Robin assigns more demanding workloads to high-performance servers  
-	Prevents underpowered servers from being overwhelmed  
-	Ensures **efficient resource utilisation** and optimal processing times  