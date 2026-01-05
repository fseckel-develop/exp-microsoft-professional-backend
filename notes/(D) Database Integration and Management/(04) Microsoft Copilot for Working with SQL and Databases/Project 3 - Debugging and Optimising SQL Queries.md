
In Activity 2, you wrote complex SQL queries for SmartShop’s inventory and sales data. Some queries may have inefficiencies or errors, including:

- Slow execution times for large datasets.
- Incorrect JOIN or WHERE clauses causing errors.
- Inefficient use of aggregate functions

1. Use Copilot to identify and correct errors in:
	- JOIN statements causing mismatched results.
	- Nested queries with incorrect syntax.
2. Use Copilot to suggest and implement optimisations such as:
	- Adding appropriate indexes to frequently queried columns.
	- Restructuring queries for improved execution plans.
	- Reducing unnecessary computations.
3. Finally, use Copilot to test and validate.
	- Run the optimised queries and compare their performance with the original versions.
	- Ensure the results are accurate and the execution time is reduced.

---
### (Query 2.2.3) Correction

Inside the `MAX()` you placed another full `SELECT COUNT(*)` subquery. SQL allows this, but it’s unnecessary, confusing, and some SQL engines reject it.

Fix:

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

This version:

- Computes delayed deliveries per supplier
- Computes the maximum delayed deliveries in a separate derived table
- Filters to suppliers matching that maximum

Even better:

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
#### (Query 1.1.1) Retrieve Product Information

(No index needed — full table scan is expected)

```sql
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
