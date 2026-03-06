## Scenario Overview

WarehouseX is experiencing performance bottlenecks when retrieving product and order sales data. Queries involving the `Products` and `Orders` tables are slow, contributing to delays in order processing and reporting.

The following query has been identified for optimization:

```sql
SELECT p.ProductName, SUM(o.Quantity) AS TotalSold
FROM Orders o
JOIN Products p ON o.ProductID = p.ProductID
WHERE p.Category = 'Electronics'
GROUP BY p.ProductName
ORDER BY TotalSold DESC;
```

### Objectives

- Identify inefficiencies using Copilot  
- Apply indexing and restructuring strategies  
- Compare execution plans before and after optimisation  

---
## Query Analysis with Copilot

### Detect Potential Inefficiencies

Using Copilot query analysis, the following areas are flagged:

- Filtering on `p.Category` without confirming an index exists  
- Joining on `ProductID` without verifying proper indexing  
- Aggregation (`SUM` + `GROUP BY`) on potentially large datasets  
- Sorting (`ORDER BY TotalSold DESC`) on computed values  

### Identified Performance Risks

- Full table scan on `Products` if `Category` is not indexed  
- High I/O cost when scanning large `Orders` table  
- Expensive grouping operation if no supporting index exists  
- Sort operation on aggregated results  

---
## Execution Plan (Before Optimisation)

Expected execution characteristics:

- Table scan on `Products` (if no index on `Category`)  
- Scan or clustered index scan on `Orders`  
- Hash match or sort for aggregation  
- Explicit sort operator for `ORDER BY`  

### Observed Issues

- High logical reads  
- Increased CPU usage during aggregation  
- Longer execution time under heavy load  

---
## Copilot Optimisation Recommendations

### Indexing Strategy

Copilot recommends adding the following indexes:

#### Index on Product Category

Improves filtering performance.

```sql
CREATE INDEX idx_products_category
ON Products (Category);
```

#### Composite Index on Orders Table

Optimises join and aggregation.

```sql
CREATE INDEX idx_orders_productid_quantity
ON Orders (ProductID, Quantity);
```

**Rationale:**

- `ProductID` supports JOIN performance  
- Including `Quantity` supports aggregation efficiency  
- Reduces key lookups  

### Query Restructuring

Copilot suggests filtering earlier and minimising processed rows.

#### Optimised Query Version

```sql
SELECT p.ProductName, SUM(o.Quantity) AS TotalSold
FROM Products p
INNER JOIN Orders o
    ON p.ProductID = o.ProductID
WHERE p.Category = 'Electronics'
GROUP BY p.ProductName
ORDER BY TotalSold DESC;
```

### Optimisation Considerations

- Start with filtered `Products` set  
- Ensure indexes support JOIN and WHERE clause  
- Reduce intermediate dataset size before aggregation  

### Alternative Aggregation Optimisation

If dataset is very large, Copilot may suggest:

#### Pre-Aggregation Strategy

Aggregate orders first before joining:

```sql
SELECT p.ProductName, o.TotalSold
FROM (
    SELECT ProductID, SUM(Quantity) AS TotalSold
    FROM Orders
    GROUP BY ProductID
) o
INNER JOIN Products p
    ON p.ProductID = o.ProductID
WHERE p.Category = 'Electronics'
ORDER BY o.TotalSold DESC;
```

**Benefits:**

- Reduces rows before joining  
- Limits join to aggregated results  
- Improves scalability for large order volumes  

---
## Execution Plan (After Optimisation)

Expected improvements:

- Index seek on `Products.Category`  
- Index seek on `Orders.ProductID`  
- Reduced logical reads  
- Lower CPU cost during aggregation  
- More efficient sort operation  

### Measurable Improvements

Track the following metrics:

- Query execution time  
- Logical reads (`SET STATISTICS IO`)  
- CPU time (`SET STATISTICS TIME`)  
- Estimated vs. actual execution plan cost  
- Wait statistics under load  

---
## Validation Strategy

To ensure optimisation success:

- Compare execution plans before and after indexing  
- Measure execution time under realistic workload  
- Test with large datasets  
- Validate correctness of aggregated totals  
- Monitor performance during peak traffic  

---
## Role of Copilot in Optimisation

Copilot assists by:

- Detecting missing indexes  
- Recommending composite indexes  
- Suggesting query refactoring  
- Identifying aggregation inefficiencies  
- Providing index creation syntax  
- Helping interpret execution plan warnings  

---
## Final Optimisation Strategy Summary

- Add index on `Products.Category`  
- Add composite index on `Orders(ProductID, Quantity)`  
- Ensure join columns are indexed  
- Consider pre-aggregation for very large datasets  
- Compare execution plans before and after changes  
- Continuously monitor query performance  

---
## Conclusion

By applying indexing, restructuring joins, and optimising aggregation strategies with Copilot’s assistance, the query becomes more scalable and efficient. These optimisations reduce I/O operations, improve execution speed, and support WarehouseX’s growing order and inventory demands.

---