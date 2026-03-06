## Objective

- Identify common performance issues in poorly written SQL queries
- Apply best practices to optimise query structure
- Refactor queries to reduce resource usage and improve execution speed
- Explain how optimisation techniques improve overall database performance

---
## Example 1: Optimising a Customer Orders Query

### Scenario

- Retail company uses a relational database to track customer orders
- A frequently executed query is causing performance issues
- The query needs to be analysed and refactored using best practices

Original Query:

```sql
SELECT * FROM orders;
```

### Step 1: Identifying Performance Issues

- Selecting all columns
    - Retrieves unnecessary data
    - Increases memory usage and I/O operations

- No filtering conditions
    - Forces the database to process every row
    - Leads to full table scans

- Poor scalability
    - Additional sorting or conditions increase processing overhead

### Step 2: Applying Best Practices

- Select only necessary columns
    - Reduces data retrieval
    - Improves efficiency

- Use a WHERE clause
    - Filters rows early
    - Minimises database workload

- Index frequently searched columns
    - Speeds up filtering and sorting operations

### Step 3: Refactored Query

```sql
SELECT order_id, order_date, total_price FROM orders
	WHERE order_date >= '2023-01-01';
```

### Step 4: Why This Refactored Query Is Better

- Retrieves only required columns, reducing unnecessary data transfer
- Filters results to recent orders, lowering the number of processed rows
- Can leverage an index on order_date for faster filtering
- Improves performance and scalability for frequent execution

---
## Example 2: Optimising a Product Search Query

### Scenario

- An e-commerce platform runs a slow product search query
- The products table stores product and inventory information
- The query must be optimised using the same principles as the example

Original Query:

```sql
SELECT * FROM products;
```

### Step 1: Performance Issues

- Selecting all columns
    - Retrieves unnecessary data
    - Increases query execution time

- No WHERE clause
    - Scans the entire table
    - Forces the database to process all rows

### Step 2: Refactored Query

```sql
SELECT name, price, stock FROM products
	WHERE 
		category = 'Electronics' 
		AND 
		price BETWEEN 50 AND 200;
```

### Step 3: Explanation of Improvements

- The refactored query limits selected columns, reducing resource usage
- Filtering by category and price range decreases the number of rows processed
- Indexes on category or price can significantly improve retrieval speed
- These changes reduce database workload and improve response times