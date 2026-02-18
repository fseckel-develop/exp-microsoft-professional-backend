## Objective

- Analyse real-world scenarios to determine if hash tables are best for efficient data retrieval  
- Justify choices based on:  
	- Hashing characteristics  
	- Performance considerations  
	- Alternative data structures  

---
## Why Hash Tables Matter

- **Advantages**  
	- O(1) average-time complexity for searches  
	- Efficient session management and caching  
	- Quick data retrieval without scanning entire datasets  

- **Limitations**  
	- Do not maintain sorted order (not suitable for range queries)  
	- Frequent collisions can slow lookups  
	- High memory usage may be a concern for very large tables  

- Learners evaluate when hash tables are preferable vs. alternative data structures  

---
## Example 1: Managing User Sessions in a Web Application

### Scenario

- Large-scale web app storing session IDs after user login  
- Sessions must be retrievable instantly without scanning database  

### Evaluating the Use of a Hash Table

- ✅ **Best Choice: Hash Table**  
	- Fast O(1) lookup of session IDs  
	- Key-value mapping fits session storage (Session ID → User data)  
	- Efficient memory usage, storing only active sessions  

- ❌ **Alternative: Arrays or Linked Lists**  
	- O(n) lookup time  
	- Inefficient for large-scale session retrieval  

### Justification

- Hash tables allow instant access to session data  
- Ensure seamless user experience and high system performance  

---

## Example 2: Implementing a Cache for Frequently Accessed Data

### Scenario

- Content delivery network caches popular web pages  
- Must retrieve cached content quickly based on URLs  

### Evaluation of Hash Table Usage

- ✅ **Best Choice: Hash Table**  
	- O(1) lookup time for fast retrieval  
	- Key-value structure (URL → Cached Page) matches caching needs  
	- Reduces redundant database queries  

- ❌ **Alternative: Tree-based Data Structures (e.g., BST)**  
	- O(log n) lookup slower than hash table  
	- Additional memory overhead for tree structure  

### Justification

- Hash tables ensure instant access to frequently requested content  
- Improve response time and reduce server load  

---
## Example 3: Indexing Records in a Relational Database

### Scenario

- Database stores millions of customer records  
- Needs fast lookups by customer ID and support for sorted range queries  

### Evaluation of Hash Table Usage

-  ❌ **Not the Best Choice: Hash Table**  
	- Does not maintain order  
	- Range queries inefficient  

- ✅ **Best Choice: B-Tree Index**  
	- Supports range-based queries efficiently  
	- Maintains sorted order  
	- Provides O(log n) lookup performance  

### Justification

- B-Tree indexes are better for database indexing  
- Ensure sorted data access and efficient range queries  

---
## Example 4: Best Data Structure for Storing API Rate Limits

### Scenario

- Track API usage limits per user  
- Support fast lookups and updates to prevent exceeding request quotas  

### Evaluation of Hash Table Usage

- ✅ **Best Choice: Hash Table**  
	- Allows O(1) lookup of each user's API usage  
	- Key-value mapping fits (User ID → API count)  
	- Efficient memory usage for active users  

-  ❌ **Alternative: Arrays or Linked Lists**  
	- O(n) lookup for each API request  
	- Inefficient for large user bases  

### Justification

- Hash tables provide instant access to API usage data  
- Enable real-time enforcement of rate limits  
- Ensure system reliability and seamless user experience  

---
## Example 5: Evaluating Hash Tables for an Online Voting System

### Scenario

- Online election system tracks votes  
- Each voter has a unique voter ID  
- Must prevent multiple votes per voter  

### Evaluation of Hash Table Usage

- ✅ **Best Choice: Hash Table with Collision Handling**  
	- O(1) lookup of voter IDs  
	- Efficient verification of voting status  
	- Handles collisions securely using chaining or other techniques  

- ❌ **Alternative: Arrays or Linked Lists**  
	- O(n) lookup for each vote  
	- Inefficient and error-prone in large-scale elections  

### Justification

- Hash tables ensure quick verification of voter eligibility  
- Collision handling prevents duplicate entries  
- Maintains fast, secure, and scalable voting system operations  