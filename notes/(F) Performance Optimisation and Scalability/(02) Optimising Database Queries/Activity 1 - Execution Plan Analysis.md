---

---
## Objective

- Analyse database query execution plans to identify performance bottlenecks
- Understand how different execution plan components impact query efficiency
- Apply indexing and query optimisation techniques to improve performance
- Explain why optimised execution plans are critical for scalable applications

---
## Example 1: Query Execution Plan in an E-Commerce Database

### Scenario

- An online store experiences slow product searches
- Customers filter products by category and price
- Execution plan is analysed to identify performance bottlenecks

```sql
EXPLAIN ANALYZE
SELECT p.name, p.price, c.category_name
FROM products p
JOIN categories c ON p.category_id = c.id
WHERE p.price < 100
ORDER BY p.price ASC;

The database generates the following query execution plan:
Seq Scan on products p  (cost=0.00..12000.00 rows=5000 width=50)
  Filter: (price < 100)
  -> Index Scan using category_index on categories c  (cost=0.30..5.00 rows=1 width=20)
     Index Cond: (c.id = p.category_id)
Sort  (cost=1000.00..1100.00 rows=5000 width=70)
```

### Step 1: Breaking Down the Execution Plan

- Sequential Scan on products
	- What it does:
		- Scans the entire products table to find rows where price < 100
	- What this plan shows:
		- Inefficient for large datasets due to full table scan
- Index Scan on categories
	- What it does:
		- Uses an index to match category_id efficiently
	- What this plan shows:
		- Optimised lookup due to existing index
- Sort Operation
	- What it does:
		- Orders the result set by price
	- What this plan shows:
		- Sorting a large number of rows can become expensive

### Step 2: Identifying Performance Issues

- Sequential Scan on products
	- Performance bottleneck due to missing index on price
- Sort operation
	- Costly when applied to large result sets
- Index Scan on categories
	- Already optimized and does not require changes

### Step 3: Optimizing the Query Execution Plan

- Create an index on price
	- Why it helps:
		- Avoids full table scans
		- Speeds up filtering and sorting
	- Implementation:
		- `CREATE INDEX price_index ON products(price);`
- Use index-supported sorting
	- Why it helps:
		- Reduces sorting overhead
	- Implementation:
		- Reuse `price_index` for `ORDER BY`
- Pagination with `LIMIT` and `OFFSET`
	- Why it helps:
		- Reduces rows processed and returned
	- Implementation:
		- `LIMIT 50 OFFSET 51`

---
## Example 2: Query Execution Plan for a Social Media Database

### Scenario

- A social media platform allows users to search friends by name
- Query filters users whose names start with "A"
- Execution plan reveals potential inefficiencies

```sql
EXPLAIN ANALYZE
SELECT u.id, u.name, f.friend_id
FROM users u
JOIN friendships f ON u.id = f.user_id
WHERE u.name LIKE 'A%';

The execution plan shows:
Seq Scan on users u  (cost=0.00..25000.00 rows=10000 width=50)
  Filter: (name LIKE 'A%')
  -> Hash Join  (cost=1500.00..3000.00 rows=10000 width=70)
     Hash Cond: (f.user_id = u.id)
     -> Seq Scan on friendships f (cost=0.00..5000.00 rows=20000 width=30)
```

### Step 1: Breaking Down the Execution Plan

- Sequential Scan on users
	- What it does:
		- Scans all users to find names starting with "A"
	- Optimised or needs improvement:
		- Needs improvement due to missing index on name
- Hash Join on friendships
	- What it does:
		- Joins users and friendships using user_id
	- Optimised or needs improvement:
		- Optimised for large datasets
- Sequential Scan on friendships
	- What it does:
		- Scans all friendship records
	- Optimised or needs improvement:
		- Needs improvement due to lack of index

### Step 2: Optimising the Query Execution Plan

- Create an index on users.name
	- Why it helps:
		- Enables fast prefix-based searches
	- Implementation:
		- `CREATE INDEX name_index ON users(name);`
- Create an index on friendships.user_id
	- Why it helps:
		- Speeds up join operations
	- Implementation:
		- `CREATE INDEX user_id_index ON friendships(user_id);`
- Use B-tree index optimised for LIKE queries
	- Why it helps:
		- Improves prefix matching performance
	- Implementation:
		- `CREATE INDEX name_btree_index ON users(name text_pattern_ops);`

### Step 3: Importance of Query Optimisation

- Indexing reduces query execution time by avoiding full table scans
- Optimized joins minimize CPU and memory usage when combining tables
- Reducing sequential scans allows databases to handle higher workloads
- Efficient execution plans ensure scalable and responsive applications