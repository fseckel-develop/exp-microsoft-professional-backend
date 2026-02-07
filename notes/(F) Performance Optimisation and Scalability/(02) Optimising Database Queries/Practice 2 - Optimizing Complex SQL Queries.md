## Step 1: Prepare for the Lab Environment

```sql
CREATE DATABASE SalesDatabase;

USE SalesDatabase;

CREATE TABLE Products (
    product_id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100),
    price DECIMAL(10,2)
);

CREATE TABLE Customers (
    customer_id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100)
);

CREATE TABLE Orders (
    order_id INT PRIMARY KEY AUTO_INCREMENT,
    customer_id INT,
    order_date DATE,
    FOREIGN KEY (customer_id) REFERENCES Customers(customer_id)
);

CREATE TABLE Sales (
    sale_id INT PRIMARY KEY AUTO_INCREMENT,
    product_id INT,
    quantity INT,
    FOREIGN KEY (product_id) REFERENCES Products(product_id)
);
```

Verify the database setup:

```sql
SELECT * FROM Products; 
SELECT * FROM Customers; 
SELECT * FROM Orders; 
SELECT * FROM Sales;
```



---
## Step 2: Identifying Bottlenecks

Original Query:

```sql
SELECT * FROM Products;
```

Analysis:

```sql
EXPLAIN SELECT * FROM Products;

-- If type: ALL appears in the plan, it indicates a full table scan.
```

Optimised Query:

```sql
SELECT name, price FROM Products WHERE price > 100;

-- Selecting specific columns reduces the amount of data processed.
```



---
## Step 3: Optimising Query Performance with Indexing

Original Query:

```sql
SELECT Orders.order_id, Customers.name  FROM Orders  
	JOIN Customers ON Orders.customer_id = Customers.customer_id;
```

Optimised Query:

```sql
CREATE INDEX idx_customer_id ON Orders (customer_id);

-- Adding an index on customer_id speeds up row lookups.
```



---
## Step 4: Avoiding Inefficiencies in Temporary Tables

Original Query:

```sql
CREATE TEMPORARY TABLE temp_sales AS  
SELECT * FROM Sales WHERE quantity > 3;

SELECT AVG(quantity) FROM temp_sales;
```

Optimised Query:

```sql
SELECT AVG(quantity) FROM Sales WHERE quantity > 3;

-- Eliminating the temporary table reduces 
-- memory usage and improves execution speed.
```



---
## **Step 5: Rewriting Nested Queries for Efficiency**

Original Query:

```sql
SELECT * FROM Products  
	WHERE product_id IN (
		SELECT product_id FROM Sales WHERE quantity > 3
	);
```

Optimised Query:

```sql
SELECT Products.* FROM Products  
	JOIN Sales ON Products.product_id = Sales.product_id  
	WHERE Sales.quantity > 3;
	
-- JOIN queries are generally faster than subqueries 
-- as they reduce redundant executions
```