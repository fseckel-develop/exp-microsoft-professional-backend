## Objective

- Understand how to analyse SQL execution plans
- Identify common performance bottlenecks in query plans
- Recommend appropriate optimisations based on execution behaviour
- Explain the importance of query optimisation for performance and scalability


---
---
## Example 1: Identifying Bottlenecks in an E-Commerce Query

### Scenario

- Online store database experiences slow performance when retrieving product data
- Frequently executed query filters products by:
	- Category
	- Price
- Execution plan reveals inefficiencies during data retrieval

```sql
EXPLAIN ANALYZE
SELECT * FROM products WHERE category = 'Electronics' AND price > 50;

The database generates the following execution plan:
Seq Scan on products  (cost=0.00..15000.00 rows=5000 width=100)
  Filter: (category = 'Electronics' AND price > 50)
Sort  (cost=500.00..600.00 rows=5000 width=100)
```

---
### Step 1: Identifying Performance Bottlenecks

- Sequential Scan on products
	- Reads all rows in the products table
	- Causes slow performance due to lack of index usage
- Filter on category and price
	- Applied after all rows are retrieved
	- Increases unnecessary data scanning
- Sorting operation
	- Sorts thousands of rows
	- Adds extra execution time

---
### Step 2: Recommended Optimisations

- Create an index on category
	- Enables faster lookups by category
	- Implementation:
		- CREATE INDEX category_idx ON products(category);
- Create a composite index on category and price
	- Improves filtering and sorting efficiency
	- Implementation:
		- CREATE INDEX category_price_idx ON products(category, price);
- Use LIMIT and OFFSET for pagination
	- Reduces number of rows returned
	- Improves response time for large result sets



---
---
## Example 2: Identifying Bottlenecks in a Social Media Query

### Scenario

- Social media platform frequently searches users by city
- Query filters users where city equals a specific value
- Execution plan shows a sequential scan on the users table

The following query is frequently used to search for users in a specific city:

```sql EXPLAIN ANALYZE
SELECT * FROM users WHERE city = 'New York';

The execution plan shows:
Seq Scan on users  (cost=0.00..20000.00 rows=10000 width=150)
  Filter: (city = 'New York')
```

---
### Step 1: Identifying Performance Bottlenecks

- Sequential Scan on users
	- Reads all rows in the users table
	- Causes slow performance due to full table scanning
	- Indicates missing index on city column

---
### Step 2: Recommended Optimisations

- Create an index on city
	- Enables direct lookups instead of scanning all rows
	- Implementation:
		- CREATE INDEX city_idx ON users(city);
- Create a covering index on city and name
	- Improves performance when retrieving user names by city
	- Implementation:
		- CREATE INDEX city_name_idx ON users(city, name);

---
### Step 3: Why Query Optimisation Matters

- Reducing full table scans significantly speeds up query execution
- Indexes allow databases to locate relevant rows efficiently
- Optimised queries improve application responsiveness
- Lower system load enables better scalability as data volume grows
- Without optimisation, performance degrades as databases expand