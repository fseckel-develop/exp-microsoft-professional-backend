## Activity Objective

WarehouseX has reported slow response times in their order processing system. The current application code suffers from:

- Inefficient loops slowing down execution.
- Redundant database calls causing bottlenecks.
- Unoptimised I/O operations affecting performance.

**Goal:** Optimise the application code to improve order processing performance. Key strategies include addressing inefficient loops, reducing redundant database calls, and optimizing I/O operations using Copilot.

---
## Step 1: Identify Inefficiencies in the Code

### Provided Application Code (Inefficient Loop)

```csharp
foreach (var order in orders){
    var product = db.Products.FirstOrDefault(p => p.Id == order.ProductId);
    Console.WriteLine($"Order {order.Id}: {product.Name} - {order.Quantity}");
}
```

### Observed Issues

1. **Redundant Database Calls:**  
   `db.Products.FirstOrDefault(...)` is executed for every order, causing multiple queries to the database even for repeated products.

2. **Inefficient Looping:**  
   Each iteration performs a database lookup, leading to a performance bottleneck as the number of orders increases.

3. **Unoptimised I/O:**  
   Console output inside the loop can block execution if many orders are processed.

---
## Step 2: Copilot-Assisted Optimisation

### Copilot Prompt Examples

- **Prompt 1:** 
  "Identify inefficiencies in this loop and suggest optimisations for better performance."
- **Prompt 2:** 
  "Refactor the code to reduce redundant database calls while maintaining functionality."
- **Prompt 3:** 
  "Optimise I/O operations for batch output to improve performance."

### Copilot Suggested Optimisations

1. **Batch Product Retrieval:**  
   Instead of querying the database for each order, retrieve all relevant products once and store them in a dictionary for quick lookup.

2. **Optimised Looping:**  
   Use a single loop over the orders with dictionary lookups to reduce time complexity.

3. **Buffered Output (Optional):**  
   Collect output in a list and print in bulk to minimise I/O overhead if needed.

---
## Step 3: Optimised Code Implementation

```csharp
// Step 1: Retrieve all products once and store in a dictionary
var productDict = db.Products
    .Where(p => orders.Select(o => o.ProductId).Contains(p.Id))
    .ToDictionary(p => p.Id);

// Step 2: Process orders using dictionary lookup
foreach (var order in orders)
{
    if (productDict.TryGetValue(order.ProductId, out var product))
    {
        Console.WriteLine($"Order {order.Id}: {product.Name} - {order.Quantity}");
    }
}
```

### Explanation of Changes

1. **Dictionary Lookup:**  
   - `ToDictionary(p => p.Id)` creates a fast-access map of products keyed by ProductId.  
   - `TryGetValue` avoids repeated database queries, reducing execution time from O(n*m) to O(n) where `n` is the number of orders and `m` is the number of products.

2. **Single Database Query:**  
   - The `Where(...)` clause fetches only products relevant to the orders, avoiding unnecessary data retrieval.

3. **Optimized Loop:**  
   - The loop now only performs in-memory lookups instead of database hits.

4. **Optional Buffered Output:**  
   - For large datasets, outputs can be collected in a list and written at once to minimize console I/O overhead.

---
## Step 4: Measuring Performance Improvement

### Before Optimisation

- Each order triggers a database query: `Total Queries = number of orders`
- Execution time increases linearly with the number of orders.
- Redundant queries and repeated I/O slow the system.

### After Optimisation

- Single database query retrieves all products: `Total Queries = 1`
- Dictionary lookup is O(1) for each order.
- Console output remains functional but can be further optimised with batching if needed.

**Estimated Performance Gain:**  
- Orders processed hundreds or thousands of times faster for large datasets.
- Reduced database load and faster response times in the order processing system.

---
## Step 5: Additional Copilot Recommendations

1. **Asynchronous Database Access:**  
   Use `ToListAsync()` and `await` when retrieving products to prevent blocking in high-traffic applications.

2. **Logging Optimisation:**  
   Replace frequent `Console.WriteLine` with logging frameworks that support asynchronous or batched writes.

3. **Caching Frequently Accessed Data:**  
   Implement an in-memory cache for products that are frequently referenced to further reduce database calls.

4. **Unit Testing Optimisations:**  
   Validate that the optimised code produces the same output as the original for all orders.

---
## Step 6: Summary of Optimisation Plan

| Issue | Copilot Suggestion | Implementation |
|-------|-----------------|----------------|
| Redundant database calls | Batch query and store products in dictionary | `db.Products.Where(...).ToDictionary(...)` |
| Inefficient loops | Replace nested queries with dictionary lookup | `foreach` with `TryGetValue` |
| Unoptimized I/O | Optionally buffer output | Collect outputs in a list and print once |
| Maintainability | Single loop, readable code | Clear structure, less redundant logic |

**Decision:** Accept  
**Reason:** This approach significantly improves performance, reduces database load, and maintains code readability and maintainability.