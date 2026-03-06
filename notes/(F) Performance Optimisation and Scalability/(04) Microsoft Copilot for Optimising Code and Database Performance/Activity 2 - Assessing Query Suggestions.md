## Example 1: Refactoring a Query with a Subquery

### Initial Query (Before Optimisation)

```sql
SELECT ProductName, OrderDate
FROM Products
WHERE ProductID IN (
    SELECT ProductID
    FROM Orders
    WHERE OrderDate > '2023-01-01'
);
```

### Step-by-Step Evaluation

#### Step 1: Copilot’s Query Analysis

- Copilot flags the use of a subquery in the `WHERE` clause.
- Subqueries can cause repeated scans of the `Orders` table.
- This may increase query cost for large datasets.
- Copilot suggests replacing the subquery with an `INNER JOIN`.

#### Step 2: Copilot’s Optimised Query

```sql
SELECT ProductName, OrderDate
FROM Products
INNER JOIN Orders
    ON Products.ProductID = Orders.ProductID
WHERE OrderDate > '2023-01-01';
```

#### Step 3: Compare Execution Plans

- **Before Optimisation:**  
	- Subquery execution  
	- Higher query cost  
	- Potential repeated scans  

- **After Optimisation:**  
	- Direct table join  
	- Reduced query cost  
	- Improved performance  

### Decision: Accept

**Reason:**  
The optimised query removes the subquery, improves execution efficiency, and scales better for larger datasets.

---
## Example 2: Applying an Index to Improve Query Performance

### Initial Query (Without Index)

```sql
SELECT ProductName, OrderDate
FROM Products
INNER JOIN Orders
    ON Products.ProductID = Orders.ProductID
WHERE OrderDate > '2023-01-01';
```

### Step-by-Step Evaluation

#### Step 1: Copilot’s Index Suggestion

- Copilot recommends adding an index on the `OrderDate` column.
- This column is frequently used in the `WHERE` clause.

#### Step 2: SQL to Apply the Index

```sql
CREATE INDEX idx_orderdate
ON Orders (OrderDate);
```

#### Step 3: Test Query Performance

- **Before Indexing:**  
	- Full table scan on `Orders`  
	- Slower filtering  

- **After Indexing:**  
	- Index seek operation  
	- Faster row filtering  
	- Reduced execution time  

#### Decision: Accept

**Reason:**  
Adding the index improves filtering performance, especially for large datasets, by reducing scan operations.

---
## Example 3: Evaluate Copilot’s Refactored Query

### Initial Query (Before Optimisation)

```sql
SELECT CustomerName, OrderDate
FROM Customers
WHERE CustomerID IN (
    SELECT CustomerID
    FROM Orders
    WHERE OrderDate < '2023-01-01'
);
```

### Copilot’s Refactored Query

```sql
SELECT CustomerName, OrderDate
FROM Customers
INNER JOIN Orders
    ON Customers.CustomerID = Orders.CustomerID
WHERE OrderDate < '2023-01-01';
```

### Step-by-Step Evaluation

#### Step 1: Analyse the Optimised Query

- The `INNER JOIN` replaces the subquery.
- Reduces complexity of query structure.
- Improves readability and maintainability.

#### Step 2: Compare Execution Plans

- **Before Optimisation:**  
	- Subquery evaluation  
	- Possible repeated scans  
	- Higher query cost  

- **After Optimisation:**  
	- Direct join between tables  
	- More efficient execution path  
	- Lower query cost  

#### Decision: Accept

**Reason:**  
The `INNER JOIN` eliminates the subquery, improving performance and reducing query complexity.

---
## Example 4: Evaluate Copilot’s Index Suggestion

### Initial Query (Without Index)

```sql
SELECT CustomerName, OrderDate
FROM Customers
INNER JOIN Orders
    ON Customers.CustomerID = Orders.CustomerID
WHERE OrderDate > '2023-06-01';
```

### Copilot’s Index Suggestion

```sql
CREATE INDEX idx_orderdate
ON Orders (OrderDate);
```

### Step-by-Step Evaluation

#### Step 1: Analyse How the Index Affects Performance

- The `OrderDate` column is used in the `WHERE` clause.
- Without an index, the database performs a full table scan.
- With an index, the database performs an index seek.

#### Step 2: Compare Execution Plans

- **Before Indexing:**  
	- Full table scan  
	- Higher I/O cost  
	- Slower execution  

- **After Indexing:**  
	- Index seek operation  
	- Reduced I/O  
	- Faster query performance  

#### Decision: Accept

**Reason:**  
The index significantly reduces filtering time and improves query efficiency, especially with growing datasets.

---