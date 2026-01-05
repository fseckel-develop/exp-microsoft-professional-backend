
To begin, review the following scenario related to building the "SmartShop Inventory System."

Imagine you are a database engineer tasked with developing the **SmartShop Inventory System** for a fictional retail company, SmartShop. This system must manage inventory data across multiple stores, providing real-time insights into stock levels, sales trends, and supplier information. The company requires:

1. A database to store and retrieve inventory data efficiently.
2. Complex queries to analyse trends and relationships between products, sales, and suppliers.
3. Optimised database operations to ensure high performance and scalability.

Your goal is to leverage Microsoft Copilot to create, debug, and optimise SQL queries, ensuring the system meets performance and accuracy requirements. This project will span three activities and culminate in a comprehensive inventory management database.


SmartShop’s initial requirements include:

- Retrieving product details such as name, price, and stock levels.
- Filtering products based on categories and availability.
- Sorting data for better readability.

---
## Step 1: Generate basic SELECT queries with Copilot

To get started, you’ll use Copilot to generate basic queries to meet these needs.

- Use Copilot to write a query to retrieve product details, including:
    - ProductName, Category, Price, and StockLevel.

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

Next, you’ll work on filtering and sorting capabilities. 

- Write queries with Copilot to filter:
    - Products in a specific category
    - Products with low stock levels
- Add sorting to display products by Price in ascending order.

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