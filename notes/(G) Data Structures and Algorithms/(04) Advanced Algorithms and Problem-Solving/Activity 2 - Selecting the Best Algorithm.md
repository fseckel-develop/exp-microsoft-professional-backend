## Objective

- Evaluate real-world backend scenarios  
- Determine whether **dynamic programming** or a **greedy algorithm** is more appropriate  
- Justify decisions based on:  
	- Problem structure  
	- Performance requirements  
	- Memory constraints  

---
## Example 1: Task Scheduling in a Distributed System

### Scenario

- A job queue system schedules tasks across multiple servers  
- Each task has:  
	- Start time  
	- End time  
	- Value (profit)  
- Goal: maximise total value  
- Constraint: tasks cannot overlap on the same server  

### Evaluating the Use of an Algorithm

- ✅ **Best Choice: Dynamic Programming**  
	- Decisions depend on previous selections  
	- Similar to the Weighted Interval Scheduling Problem  
	- Requires evaluating compatible task combinations  
	- Must consider overlapping constraints  

- ❌ **Alternative: Greedy Algorithm**  
	- Local decisions (e.g., earliest finish time or highest value first)  
	- May miss optimal long-term combinations  

### Justification

- The problem requires exploring multiple subproblem combinations  
- Optimal result depends on cumulative decisions  
- Dynamic programming ensures globally optimal scheduling  
- Greedy approaches cannot reliably account for best long-term value  

---
## Example 2: Distributing API Requests (Load Balancing)

### Scenario

- A web server distributes incoming API requests across multiple servers  
- Must make real-time decisions  
- Goal: distribute load efficiently  

### Evaluating the Use of an Algorithm

- ✅ **Best Choice: Greedy Algorithm**  
	- Select the least-loaded server at each step  
	- Fast, real-time decision-making  
	- No need to evaluate all possible combinations  

- ❌ **Alternative: Dynamic Programming**  
	- Unnecessary overhead  
	- Would introduce latency  
	- Storing intermediate states provides little benefit  

### Justification

- Speed and simplicity are critical  
- Each decision can be made independently  
- Local optimal choice (least-loaded server) is sufficient  
- Greedy strategy provides efficient and scalable load distribution  

---
## Example 3: Scenario-Based Algorithm Selection

### Scenario

- A logistics app aims to maximise the number of delivery stops  
- Must respect delivery time windows  
- Goal: maximise completed stops within daily constraints  

### Evaluating the Use of an Algorithm

- ✅ **Best Choice: Dynamic Programming**  
	- Involves time-window constraints  
	- Each decision affects future feasibility  
	- Requires evaluating multiple delivery combinations  

- ❌ **Alternative: Greedy Algorithm**  
	- May choose earliest or shortest stop first  
	- Could block higher-value or more feasible combinations later  

### Justification

- Delivery scheduling involves dependent decisions  
- Time-window constraints create overlapping subproblems  
- Dynamic programming can evaluate and store optimal substructures  
- Ensures a globally optimal solution rather than a locally optimal one  

---
## Example 4: Optimising Memory Usage in a Caching System

### Scenario

- A caching system stores API responses based on input parameters  
- Many requests share identical or similar parameters  
- Must avoid recomputing known responses  

### Evaluating the Use of an Algorithm

- ✅ **Best Choice: Dynamic Programming (Memoization)**  
	- Stores results of previous computations  
	- Reuses stored responses for identical inputs  
	- Avoids redundant processing  

- ❌ **Alternative: Greedy Algorithm**  
	- Makes local decisions without storing history  
	- Cannot manage shared state or previously computed results  

### Justification

- This is a classic memoization use case  
- Subproblems repeat frequently  
- Storing intermediate results improves performance significantly  
- Dynamic programming reduces recomputation and server load  