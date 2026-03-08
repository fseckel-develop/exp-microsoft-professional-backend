## Objective

- analyse real-world scenarios to determine whether to use a **directed or undirected graph**
- justify choices based on the **nature of relationships and data flow**  
- understand graph usage in workflow, social networks, navigation, and system modelling  

---
## Example 1: API Workflow Dependencies

**Scenario**:
- API calls in a workflow depend on the completion of previous tasks  
- Execution order must be strictly followed to avoid failures  

**Graph Type Selection**:
- ✅ Directed Graph  
- ❌ Undirected Graph  

**Justification**:
- Directed edges represent **one-way dependencies** between API calls  
- Ensures tasks execute in the correct sequence  
- Undirected graphs are unsuitable because API calls are not bidirectional  

---
## Example 2: Social Network Friendships

**Scenario**:
- Users can mutually connect on a social media platform  
- If User A adds User B as a friend, User B automatically becomes a friend of User A  

**Graph Type Selection**:
- ❌ Directed Graph  
- ✅ Undirected Graph  

**Justification**:
- Undirected edges capture **bidirectional relationships**  
- Friendships are mutual and equal  
- Directed edges would incorrectly model friendships as one-way connections  

---
## Example 3: City Road Map Navigation

**Scenario**:
- City traffic system tracks road connections between intersections  
- Some roads are two-way, others are one-way  

**Graph Type Selection**:
- ✅ Combination of Directed and Undirected Graphs  

**Justification**:
- Undirected edges represent **two-way streets**  
- Directed edges represent **one-way streets**  
- Combination ensures correct navigation and models real-world traffic flow accurately  

---
## Example 4: Online Learning Platform

**Scenario**:
- Tracks course progress where students must complete prerequisites for advanced courses 

**Graph Type Selection**:
- ✅ Directed Graph  
- ❌ Undirected Graph  

**Justification**:
- Courses must be completed in a **specific order**  
- Example: "Intro to Programming" → "Data Structures" → "Algorithms"  
- Relationships are **not bidirectional**; later courses do not allow bypassing earlier ones  

---
## Example 5: File Sharing Network

**Scenario**:
- Peer-to-peer file-sharing network allows users to share files mutually  

**Graph Type Selection**:
- ❌ Directed Graph  
- ✅ Undirected Graph  

 **Justification**:
- File-sharing connections are **mutual**  
- If User A can access User B’s files, User B can also access User A’s  
- Undirected graphs ensure **equal access** and reflect the decentralized network structure  