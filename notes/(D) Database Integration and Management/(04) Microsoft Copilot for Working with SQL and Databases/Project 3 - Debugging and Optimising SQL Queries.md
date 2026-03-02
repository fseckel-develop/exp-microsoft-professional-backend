## Project Stage Overview

In Activity 2, you developed advanced SQL queries involving JOIN operations, subqueries, and aggregations. As the SmartShop database grows, some of these queries may reveal performance issues or logical inefficiencies.

Common challenges may include:

- Slow query performance when handling large datasets
- Incorrect JOIN conditions leading to duplicated or missing records
- Subqueries that are overly complex or syntactically problematic
- Inefficient aggregation strategies

This activity focuses on improving reliability and performance using Copilot to review, correct, and optimise the SQL logic.

---
## Objectives for This Activity

#### Identify and Correct Query Issues

- Use Copilot to:
	- Fix JOIN conditions that produce mismatched or unintended results
	- Correct nested queries with unnecessary or invalid subquery structures
	- Simplify overly complex logic

#### Implement Performance Optimisations

- Use Copilot to improve execution efficiency by:
	- Creating indexes on frequently filtered or joined columns
	- Introducing composite indexes when filtering and sorting occur together
	- Rewriting nested subqueries using JOINs or CTEs
	- Reducing redundant calculations

#### Validate and Compare Performance

- Finally:
	- Execute both original and optimised queries
	- Compare execution plans and runtime performance
	- Confirm output accuracy
	- Ensure improved efficiency without altering business logic

---
## Query Corrections and Optimisations

### (Query 2.2.3) Correction

In the original version, the MAX() clause contained a deeply nested SELECT COUNT(\*) expression. Although technically valid in some engines, this structure is difficult to read, computationally expensive, and potentially unsupported in stricter SQL implementations.
The revised version separates the aggregation logic into clearer derived tables, improving readability and maintainability.

```sql
SELECT SupplierName, DelayedDeliveries
FROM (
    SELECT 
        sup.SupplierName,
        (
            SELECT COUNT(*)
            FROM Deliveries d
            WHERE d.SupplierID = sup.SupplierID
              AND d.DeliveryDate > d.ExpectedDate
        ) AS DelayedDeliveries
    FROM Suppliers sup
) AS DelayCounts
WHERE DelayedDeliveries = (
    SELECT MAX(DelayedCount)
    FROM (
        SELECT 
            (
                SELECT COUNT(*)
                FROM Deliveries d
                WHERE d.SupplierID = sup2.SupplierID
                  AND d.DeliveryDate > d.ExpectedDate
            ) AS DelayedCount
        FROM Suppliers sup2
    ) AS Counts
);
```

An even more efficient approach eliminates correlated subqueries entirely 
by using JOIN + GROUP BY:

```sql
SELECT sup.SupplierName, COUNT(*) AS DelayedDeliveries
FROM Suppliers sup
JOIN Deliveries d
    ON d.SupplierID = sup.SupplierID
   AND d.DeliveryDate > d.ExpectedDate
GROUP BY sup.SupplierName
HAVING COUNT(*) = (
    SELECT MAX(DelayedCount)
    FROM (
        SELECT COUNT(*) AS DelayedCount
        FROM Deliveries
        WHERE DeliveryDate > ExpectedDate
        GROUP BY SupplierID
    ) AS T
);
```

---
## Indexing Strategy and Query Improvements

#### (Query 1.1.1) Retrieve Product Information

```sql
-- No index needed — full table scan is expected

-- Query 1.1.1
SELECT ProductName, Category, Price, StockLevel
FROM Products;
```

#### (Query 1.2.1) Filter by Category

```sql
-- Index for filtering by Category
CREATE INDEX idx_products_category ON Products(Category);

-- Query 1.2.1
SELECT ProductName, Category, Price, StockLevel
FROM Products
WHERE Category = 'Electronics';
```

#### (Query 1.2.2) Filter by StockLevel

```sql
-- Index for filtering by StockLevel
CREATE INDEX idx_products_stocklevel ON Products(StockLevel);

-- Query 1.2.2
SELECT ProductName, Category, Price, StockLevel
FROM Products
WHERE StockLevel < 10;
```

#### (Query 1.2.3) Sort by Price

```sql
-- Index for sorting by Price
CREATE INDEX idx_products_price ON Products(Price);

-- Query 1.2.3
SELECT ProductName, Category, Price, StockLevel
FROM Products
ORDER BY Price ASC;
```

#### (Query 1.2.4) Filter by Category + Sort by Price

```sql
-- Composite index for filtering + sorting
CREATE INDEX idx_products_category_price ON Products(Category, Price);

-- Query 1.2.4
SELECT ProductName, Category, Price, StockLevel
FROM Products
WHERE Category = 'Home Appliances'
ORDER BY Price ASC;
```

#### (Query 2.1.1) Join Products, Sales, Suppliers

```sql
-- Indexes for join performance
CREATE INDEX idx_products_productid ON Products(ProductID);
CREATE INDEX idx_products_supplierid ON Products(SupplierID);
CREATE INDEX idx_sales_productid ON Sales(ProductID);
CREATE INDEX idx_suppliers_supplierid ON Suppliers(SupplierID);

-- Query 2.1.1
SELECT 
	p.ProductName, 
	p.Category, 
	s.SaleDate, 
	s.StoreLocation, 
	s.UnitsSold, 
	sup.SupplierName 
FROM Products p 
LEFT JOIN Sales s ON p.ProductID = s.ProductID 
LEFT JOIN Suppliers sup ON p.SupplierID = sup.SupplierID;
```

#### (Query 2.1.2) Products + Sales

```sql
-- Indexes for join performance
CREATE INDEX idx_sales_productid ON Sales(ProductID);

-- Query 2.1.2
SELECT 
    p.ProductName,
    s.SaleDate,
    s.StoreLocation,
    s.UnitsSold
FROM Products p
LEFT JOIN Sales s ON p.ProductID = s.ProductID;
```

#### (Query 2.2.3) Optimised Nested Query (CTE Version)

```sql
-- Indexes for delivery delay calculations
CREATE INDEX idx_deliveries_supplierid ON Deliveries(SupplierID);
CREATE INDEX idx_deliveries_dates ON Deliveries(SupplierID, DeliveryDate, ExpectedDate);

-- Query 2.2.3 (Optimised)
WITH Delays AS (
    SELECT SupplierID, COUNT(*) AS DelayedDeliveries
    FROM Deliveries
    WHERE DeliveryDate > ExpectedDate
    GROUP BY SupplierID
),
MaxDelay AS (
    SELECT MAX(DelayedDeliveries) AS MaxDelayed
    FROM Delays
)
SELECT s.SupplierName, d.DelayedDeliveries
FROM Delays d
JOIN Suppliers s ON s.SupplierID = d.SupplierID
JOIN MaxDelay m ON d.DelayedDeliveries = m.MaxDelayed;
```

#### (Query 2.2.4) Total Revenue per Product

```sql
-- Indexes for join + price lookup
CREATE INDEX idx_sales_productid ON Sales(ProductID);
CREATE INDEX idx_products_price ON Products(Price);

-- Query 2.2.4
SELECT 
    p.ProductName,
    SUM(s.UnitsSold * p.Price) AS TotalRevenue
FROM Products p
JOIN Sales s ON p.ProductID = s.ProductID
GROUP BY p.ProductID, p.ProductName;
```

#### (Query 2.2.5) Average Units Sold per Store

```sql
-- Index for grouping by StoreLocation
CREATE INDEX idx_sales_storelocation ON Sales(StoreLocation);

-- Query 2.2.5
SELECT 
    StoreLocation,
    AVG(UnitsSold) AS AvgUnitsSold
FROM Sales
GROUP BY StoreLocation;
```

#### (Query 2.2.6) Maximum Stock Level Across Stores

```sql
-- Index for grouping by ProductID
CREATE INDEX idx_inventory_productid ON Inventory(ProductID);

-- Query 2.2.6
SELECT 
    ProductID,
    MAX(StockLevel) AS MaxStockAcrossStores
FROM Inventory
GROUP BY ProductID;
```

---
## Final Validation Approach

To confirm optimisation effectiveness:

1. Compare execution plans (EXPLAIN or equivalent).
2. Measure runtime before and after indexing.
3. Confirm identical result sets.
4. Evaluate reduced I/O scans and improved join strategies.