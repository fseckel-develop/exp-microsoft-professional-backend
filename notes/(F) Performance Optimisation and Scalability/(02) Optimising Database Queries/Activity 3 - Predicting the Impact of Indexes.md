## Objective

- analyse how clustered and non-clustered indexes affect query performance
- identify performance bottlenecks in SQL queries 
- predict how different indexing strategies improve filtering and sorting efficiency
- consider potential trade-offs


---
---
## Example 1: Impact of Indexing on a Customer Orders Query

## Scenario

- Retail company tracks customer orders in a relational database
- Frequently executed query retrieves customer orders by date
- Query filters by customer and sorts by order date

---
## Step 1: Identifying Performance Bottlenecks

- Query behaviour
	- Filters on `customer_id`
	- Sorts results by `order_date` in descending order
- Identified problem
	- Without indexes, the database performs a full table scan
	- Sorting requires additional processing time
	- Performance degrades as table size grows

---
## Step 2: Predicting the Impact of Indexing

- Clustered Index on `order_date`
	- Physically orders rows by date
	- Improves sorting performance
- Non-Clustered Index on `customer_id`
	- Enables fast lookup of customer-specific orders
	- Avoids scanning all rows
- Combined effect
	- Filtering and sorting are both optimised
	- Query execution becomes significantly faster



---
---
## Example 2: Impact of Indexing on a Product Database

## Scenario: Optimising Product Searches

- E-commerce platform manages a product catalog
- Query retrieves products within a price range
- Results are sorted alphabetically by product name

---
## Task 1: Predicted Impact of Indexing

- Clustered Index on `name`
	- Products are physically stored in name order
	- Sorting by name becomes faster
- Non-Clustered Index on `price`
	- Database can efficiently filter products within a price range
	- Reduces need for full table scans

---
## Task 2: Explanation of Indexing Strategy

- Indexing `name` and `price` improves performance by accelerating both sorting and filtering
- A clustered index on `name` ensures alphabetical storage, making ordered retrieval efficient
- A non-clustered index on `price` allows fast range-based searches
- Trade-offs include increased storage usage and slower insert operations
- Careful index selection balances read performance with maintenance costs