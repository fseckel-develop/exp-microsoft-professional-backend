## Objective

- Analyse query patterns to design effective indexing strategies
- Distinguish between clustered and non-clustered indexes
- Justify indexing decisions based on performance, storage, and maintenance trade-offs

---
## Example 1: Indexing Strategy for a Customer Orders Database

## Scenario

- Small online store tracking customer orders
- Relational database with an `orders` table
- Performance optimisation focuses on frequent query patterns

## Step 1: Understanding Query Patterns

### Commonly executed queries

- Retrieve recent orders for a customer

```sql
SELECT * FROM orders 
	WHERE customer_id = 123 
	ORDER BY order_date DESC;
```

- Get all pending orders

```sql
SELECT * FROM orders 
	WHERE status = 'Pending';
```

- Find high-value orders

```sql
SELECT * FROM orders 
	WHERE total_price > 100;
```

### Identified Problems

- Filtering and sorting without indexes causes full table scans
- Performance degrades as the orders table grows
- Range queries become expensive on large datasets

## Step 2: Selecting the Best Indexes

- customer_id
	- Non-clustered index
	- Speeds up customer-specific order lookups
- order_date
	- Clustered index
	- Optimizes sorting and filtering by date
- status
	- Non-clustered index
	- Improves performance for status-based filtering
- total_price
	- Non-clustered index
	- Supports efficient range queries

## Step 3: Why These Indexes Work

- Non-clustered index on `customer_id`
	- Reduces lookup time for customer order history
- Clustered index on `order_date`
	- Physically orders data to optimize sorting
- Non-clustered index on `status`
	- Efficiently filters orders by processing state
- Non-clustered index on `total_price`
	- Accelerates price-based searches without changing table order

---
## Example 2: Indexing Strategy for a Product Catalog Database

## Scenario:

- Small business managing product data
- Queries frequently filter by category, price range, and stock level
- Goal is to reduce scan time and improve read performance

## Step 1: Understanding Query Patterns

### Commonly executed queries

- Search by category

```sql
SELECT * FROM products 
	WHERE category = 'Electronics';
```

- Search by price range

```sql
SELECT * FROM products 
	WHERE price BETWEEN 50 AND 200;
```

- Identify low-stock products

```sql
SELECT * FROM products 
	WHERE stock_quantity < 10;
```

### Identified problems

- Frequent filtering leads to repeated full table scans
- Performance slows as the product catalog grows

## Step 2: Best Indexes for the Product Catalog

- category
	- Non-clustered index
	- Category-based filtering is a common access pattern
- price
	- Non-clustered index
	- Range queries benefit from indexed lookups
- stock_quantity
	- Non-clustered index
	- Enables fast identification of low-stock items

## Step 3: Explanation of Indexing Strategy

- Indexing improves filtering efficiency for category, price, and stock queries
- Non-clustered indexes allow fast lookups without altering physical table order
- Range-based and threshold queries execute faster with indexed columns
- Indexes reduce unnecessary full table scans and improve scalability
- Trade-offs include increased storage usage and slower write operations, 
  requiring careful index selection