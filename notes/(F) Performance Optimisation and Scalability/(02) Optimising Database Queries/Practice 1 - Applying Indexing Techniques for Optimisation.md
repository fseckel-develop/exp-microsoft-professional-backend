## Step 1: Prepare for the Lab Environment

```sql
CREATE DATABASE SalesDB;
USE SalesDB;

CREATE TABLE Orders (
    OrderID INT AUTO_INCREMENT PRIMARY KEY,
    OrderDate DATETIME,
    CustomerID INT,
    TotalAmount DECIMAL(10, 2)
);

DELIMITER $$

CREATE PROCEDURE PopulateOrders()
BEGIN
    DECLARE i INT DEFAULT 1;
    WHILE i <= 3000 DO
        INSERT INTO Orders (OrderDate, CustomerID, TotalAmount)
        VALUES (
            DATE_ADD('2024-01-01', INTERVAL FLOOR(RAND() * 90) DAY), 
            -- Random date in Quarter 1 2024
            FLOOR(RAND() * 100) + 1, -- Random CustomerID between 1 and 100
            ROUND(RAND() * 1000, 2)  -- Random TotalAmount between 0 and 1000
        );
        SET i = i + 1;
    END WHILE;
END $$

DELIMITER ;

CALL PopulateOrders();
```

To confirm that the data has been inserted

```sql 
SELECT COUNT(*) AS TotalRows FROM Orders;
```



---
## Step 2: Measure Baseline Query Performance

Enable query timing by running:

```sql
SET PROFILING = 1;
```

Execute the following query and measure its execution time 

```sql
SELECT * FROM Orders  
	WHERE OrderDate BETWEEN '2024-02-01' AND '2024-02-15'
	AND TotalAmount > 500;
```

View the profiling information by running:

```sql
SHOW PROFILES;
```



---
## Step 3: Create Indexes

Create a single-column index on the OrderDate column:

```sql
CREATE INDEX idx_order_date ON Orders(OrderDate);
```

Create another single-column index on the CustomerID column:

```sql
CREATE INDEX idx_customer_id ON Orders(CustomerID);
```

Verify the indexes were created by running:

```sql
SHOW INDEX FROM Orders;
```



---
## Step 4: Measure Query Performance After Indexing

Rerun the same query from Step 2: 

```sql
SELECT * FROM Orders  
	WHERE OrderDate BETWEEN '2024-02-01' AND '2024-02-15'   
	AND TotalAmount > 500;
```

View the profiling information again using:

```sql
SHOW PROFILES;
```

Note the updated execution time and compare it to the baseline. 
It should show some improvement



---
## Step 5: Analyse Query Execution Plans

Use the EXPLAIN command to analyse the query:

```sql
EXPLAIN SELECT * FROM Orders  
	WHERE OrderDate BETWEEN '2024-02-01' AND '2024-02-15'   
	AND TotalAmount > 500;
```



---
## **Step 6: Experiment with Composite Indexes**

Drop the existing single-column index on CustomerID:

```sql
DROP INDEX idx_customer_id ON Orders;
```

Create a composite index on OrderDate and TotalAmount:

```sql
CREATE INDEX idx_order_date_total ON Orders(OrderDate, TotalAmount);
```

Rerun the query from Step 2 and measure the execution time again:

```sql
SELECT * FROM Orders  
	WHERE OrderDate BETWEEN '2024-02-01' AND '2024-02-15'   
	AND TotalAmount > 500;
```

Use the EXPLAIN command to confirm that the composite index is being used:

```sql
EXPLAIN SELECT * FROM Orders  
	WHERE OrderDate BETWEEN '2024-02-01' AND '2024-02-15'   
	AND TotalAmount > 500;
```