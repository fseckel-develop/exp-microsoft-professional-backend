# (G32) GraphCampusNavigator

A console application demonstrating **graph structures, traversal algorithms, and shortest-path search** in a campus navigation scenario. The project models connected locations as graphs and compares DFS, BFS, Dijkstra, and A*.

---
## Course Context

**Course**: Data Structures and Algorithms  
**Section**: Graph Structures and Traversal

This project demonstrates how graphs model connected data more naturally than linear or hierarchical structures. It shows both **unweighted traversal** for exploration and **weighted pathfinding** for route calculation in a realistic navigation-style domain.

---
## Concepts Demonstrated

- Directed and undirected graph relationships
- Adjacency-list graph representations
- Depth-First Search (DFS)
- Breadth-First Search (BFS)
- Dijkstra shortest-path algorithm
- A* pathfinding with a heuristic
- Weighted vs unweighted graph modeling

---
## Project Structure

- **Graphs** – Graph abstractions and implementations for weighted and unweighted graphs  
- **Algorithms** – Traversal and pathfinding algorithms  
- **Models** – Domain models for places, edges, datasets, and path results  
- **Scenarios** – Demo runs for traversal, Dijkstra, and A*  
- **Presentation** – Console output helpers for graph data and results  
- **Program.cs** – Application entry point and scenario runner

---
## How the Application Works

1. Campus locations are modeled as nodes in a graph.
2. Paths between locations are represented as edges with optional weights.
3. DFS and BFS demonstrate graph traversal for exploration.
4. Dijkstra computes the shortest weighted route.
5. A* uses a heuristic to optimise pathfinding.
6. The console output compares traversal behaviour and path results.