
In Activity 1, you wrote basic SQL queries to retrieve and filter inventory data. Now, SmartShop has additional requirements:

- Analysing sales trends by joining product and sales data.
- Generating reports on supplier performance using aggregate functions.
- Combining data from multiple tables to track inventory levels across stores.

SmartShop’s new needs include:

- Tracking product sales by date and store.
- Identifying top-performing suppliers based on delivered stock.
- Combining inventory data across stores for consolidated reporting.

For this activity you will not need the actual data in the table. However, you should apply your understanding of queries and the structure of the table in our scenario to execute the queries.

---
## Step 1: Write multi-table JOIN queries with Copilot

To get started, you’ll use Copilot to generate multi-table JOIN queries.

- Use Copilot to write the query to join the Products, Sales, and Suppliers tables.
- Write a query to display:
    - ProductName, SaleDate, StoreLocation, and UnitsSold.

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
## Step 2: Implement nested queries and aggregation

Next, you’ll implement nested queries and aggregation.

- Write subqueries with Copilot to:
    - Calculate total sales for each product.
    - Identify suppliers with the most delayed deliveries.
- Use aggregate functions (e.g., SUM, AVG, MAX) to analyse trends and summarise data.

#### (Query 2.2.1) Calculate Total Sales for Each Product:

```sql
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

```sql
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

#### (Query 2.2.3) Identify the supplier(s) with the **maximum** delays:

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




