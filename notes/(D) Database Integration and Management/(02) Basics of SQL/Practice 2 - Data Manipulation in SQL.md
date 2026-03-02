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
## Step 2: INSERT Operation

```sql
INSERT INTO Products 
	(ProductName, Category, Brand, Price, StockQuantity) 
VALUES 
	('Wireless Mouse', 'Electronics', 'TechPro', 25.00, 100);
```


---
## Step 3: UPDATE Operation

```sql
UPDATE Products 
	SET Price = 55000 
	WHERE ProductName = 'Laptop X1' AND Brand = 'TechPro';
```


---
## Step 4:  DELETE Operation

```sql
DELETE FROM Products
	WHERE Brand = 'FastTrack';
```


---
## Step 5: Safe SQL Practice

(1) Updating without WHERE clause:

```sql
UPDATE Products 
	SET Price = 30.00;

-- Updates the Price of every product
```


(2) Undo changes: 

```sql
ROLLBACK;
```


(3) Updating specifically with WHERE clause:

```sql
UPDATE Products 
	SET Price = 30.00
	WHERE ProductName = 'Wireless Mouse';
```