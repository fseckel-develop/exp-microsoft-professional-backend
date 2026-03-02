## Project Stage Overview

In Activity 1, you focused on basic SELECT queries to retrieve and filter inventory data. Activity 2 expands the system’s analytical capabilities to support more advanced business insights.

SmartShop now requires deeper analysis across multiple related tables, including:

- Examining sales patterns over time and by store location
- Evaluating supplier reliability and performance
- Consolidating inventory data from multiple stores for reporting

This stage introduces JOIN operations, subqueries, and aggregate functions to generate more meaningful insights from interconnected data.
Although actual data is not required for execution, you should rely on the established database structure and apply your SQL knowledge to construct accurate queries.

### Expanded Functional Goals

SmartShop now needs to:

- Monitor product sales by store and date
- Identify suppliers with frequent delivery delays
- Calculate revenue and performance metrics
- Analyse stock levels across different store locations

---
## Step 1: Write Multi-Table JOIN Queries with Copilot

Begin by generating JOIN queries that connect related tables within the SmartShop database.
- Create a query joining the **Products**, **Sales**, and **Suppliers** tables.
- Display key sales details including:
    - ProductName
    - SaleDate
    - StoreLocation
    - UnitsSold

#### (Query 2.1.1) Join Products, Sales, and Suppliers:

```sql
SELECT 
    p.ProductName,
    p.Category,
    s.SaleDate,
    s.StoreLocation,
    s.UnitsSold,
    sup.SupplierName
FROM Products p
JOIN Sales s 
    ON p.ProductID = s.ProductID
JOIN Suppliers sup
    ON p.SupplierID = sup.SupplierID;
```

#### (Query 2.1.2) Display ProductName, SaleDate, StoreLocation, and UnitsSold:

```sql
SELECT 
    p.ProductName,
    s.SaleDate,
    s.StoreLocation,
    s.UnitsSold
FROM Products p
JOIN Sales s
    ON p.ProductID = s.ProductID;
```

---
## Step 2: Implement Nested Queries and Aggregation

This section introduces subqueries and aggregate functions to produce summary-level insights.
Use Copilot to construct queries that:

- Compute total sales per product
- Detect suppliers with the highest number of delayed deliveries
- Apply aggregate functions such as **SUM, AVG, MAX, and COUNT** to summarise performance

#### (Query 2.2.1) Calculate Total Sales for Each Product:

```
SELECT 
    p.ProductName,
    (
        SELECT SUM(s.UnitsSold)
        FROM Sales s
        WHERE s.ProductID = p.ProductID
    ) AS TotalUnitsSold
FROM Products p;
```

#### (Query 2.2.2) Count delays per supplier (subquery):

```
SELECT 
    sup.SupplierName,
    (
        SELECT COUNT(*)
        FROM Deliveries d
        WHERE d.SupplierID = sup.SupplierID
          AND d.DeliveryDate > d.ExpectedDate
    ) AS DelayedDeliveries
FROM Suppliers sup;
```

#### (Query 2.2.3) Identify the supplier(s) with the maximum delays:

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
    SELECT MAX(
        (
            SELECT COUNT(*)
            FROM Deliveries d
            WHERE d.SupplierID = sup2.SupplierID
              AND d.DeliveryDate > d.ExpectedDate
        )
    )
    FROM Suppliers sup2
);
```

#### (Query 2.2.4) Total revenue per product:

```sql
SELECT 
    p.ProductName,
    SUM(s.UnitsSold * p.Price) AS TotalRevenue
FROM Products p
JOIN Sales s ON p.ProductID = s.ProductID
GROUP BY p.ProductName;
```

#### (Query 2.2.5) Average units sold per store:

```sql
SELECT 
    StoreLocation,
    AVG(UnitsSold) AS AvgUnitsSold
FROM Sales
GROUP BY StoreLocation;
```

#### (Query 2.2.6) Maximum stock level across all stores:

```sql
SELECT 
    ProductID,
    MAX(StockLevel) AS MaxStockAcrossStores
FROM Inventory
GROUP BY ProductID;
```