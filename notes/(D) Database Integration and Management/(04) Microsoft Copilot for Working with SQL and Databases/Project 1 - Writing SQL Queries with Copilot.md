## Project Stage Overview

In this scenario, you are acting as a database engineer responsible for designing the **SmartShop Inventory System**, a retail inventory database for a fictional company called SmartShop.

The system must support multiple store locations and provide accurate, real-time visibility into:

- Current stock levels
- Product performance and sales patterns
- Supplier-related data

To meet business expectations, the solution must:

- Store and retrieve inventory records efficiently
- Support advanced SQL queries for analysing relationships between products and stock
- Maintain strong performance and scalability as data grows

Throughout this project, Microsoft Copilot will be used to assist in generating, refining, and optimising SQL queries. The work is structured into three activities that will gradually build a complete inventory database system.

### Core Functional Requirements

SmartShop initially requires the ability to:

- Display essential product information (name, category, price, stock levels)
- Filter products by category or availability
- Organise results for clearer readability

---
## Step 1: Generate Basic SELECT Queries with Copilot

The first task is to use Copilot to produce foundational SELECT statements that retrieve product information from the database. Create a query that returns:

- ProductName
- Category
- Price
- StockLevel

#### (Query 1.1.1) Retrieving Product Information:

```sql
SELECT 
    ProductName,
    Category,
    Price,
    StockLevel
FROM Products;
```

---
## Step 2: Implementation of Filtering and Sorting

The next phase introduces filtering and ordering operations to refine results.
Using Copilot, generate queries that:

- Display products belonging to a chosen category
- Identify items with low stock
- Sort results by Price in ascending order

#### (Query 1.2.1) Filter Products in a Specific Category:

```sql
SELECT 
    ProductName,
    Category,
    Price,
    StockLevel
FROM Products
WHERE Category = 'Electronics';
```

#### (Query 1.2.2) Filter Products With Low Stock Levels:

```sql
SELECT 
    ProductName,
    Category,
    Price,
    StockLevel
FROM Products
WHERE StockLevel < 10;
```

#### (Query 1.2.3) Sort Products by Price (Ascending):

```sql
SELECT 
    ProductName,
    Category,
    Price,
    StockLevel
FROM Products
ORDER BY Price ASC;
```

#### (Query 1.2.4) Combine Filtering and Sorting:

```sql
SELECT 
    ProductName,
    Category,
    Price,
    StockLevel
FROM Products
WHERE Category = 'Home Appliances'
ORDER BY Price ASC;
```