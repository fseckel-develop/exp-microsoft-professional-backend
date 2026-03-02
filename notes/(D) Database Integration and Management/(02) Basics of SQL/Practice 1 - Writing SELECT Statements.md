## Step 1: Configure the Database Structure

```sql
CREATE DATABASE StoreDB;
USE StoreDB;
```

```sql
CREATE TABLE Products (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    ProductName VARCHAR(50),
    Category VARCHAR(50),
    Brand VARCHAR(50),
    Price DECIMAL(10,2),
    StockQuantity INT
);
```

```sql
INSERT INTO Products 
	(ProductName, Category, Brand, Price, StockQuantity) 
VALUES
	('Laptop X1', 'Electronics', 'TechPro', 1200.00, 15),
	('Smartphone A5', 'Electronics', 'MobileMax', 800.00, 25),
	('Office Chair', 'Furniture', 'ComfortLine', 150.00, 40),
	('Desk Lamp', 'Furniture', 'BrightHome', 45.00, 60),
	('Running Shoes', 'Sportswear', 'FastTrack', 120.00, 30);
```

---
## Step 2: Writing SELECT Statements

(1) Retrieve all columns:

```sql
SELECT * FROM Products;
```

(2) Retrieve specific columns:

```sql
SELECT ProductName, Price FROM Products;
```

(3) Retrieve unique categories:

```sql
SELECT DISTINCT Category FROM Products;
```

---
## Step 3: Using the WHERE Clause

(1) Products in Electronics category:

```sql
SELECT * FROM Products WHERE Category = 'Electronics';
```

(2) Furniture products with price > 100:

```sql
SELECT * FROM Products WHERE Category = 'Furniture' AND Price > 100;
```

(3) Products with stock >20 and price <1000:

```sql
SELECT * FROM Products WHERE StockQuantity > 20 AND Price < 1000;
```

---
## Step 4: Sorting with the ORDER BY Clause

(1) Sort by ProductName:

```sql
SELECT * FROM Products ORDER BY ProductName ASC;
```

(2) Electronics products sorted by price descending:

```sql
SELECT * FROM Products WHERE Category = 'Electronics' ORDER BY Price DESC;
```

(3) Top 3 most expensive products:

```sql
SELECT * FROM Products ORDER BY Price DESC LIMIT 3;
```

---
## Step 5: More Complex Queries

(1) Furniture products sorted by stock quantity:

```sql
SELECT * FROM 
	Products 
	WHERE 
		Category = 'Furniture' AND StockQuantity > 10 
	ORDER BY 
		StockQuantity DESC;
```

(2) Price between 100 and 900 sorted by product name:

```sql
SELECT * FROM 
	Products 
	WHERE 
		Price BETWEEN 100 AND 900 
	ORDER BY 
		ProductName ASC;
```

---
## Step 6: Aggregate Queries

(1) Total Inventory Value by Category via SQL Aggregate Query:

```sql
SELECT Category, 
	SUM(Price * StockQuantity) AS TotalInventoryValue 
	FROM Products 
	GROUP BY Category;
```

(2) Product Count by Category via SQL Aggregate Query:

```sql
SELECT Category, 
	COUNT(*) AS ProductCount 
	FROM Products 
	GROUP BY Category;
```