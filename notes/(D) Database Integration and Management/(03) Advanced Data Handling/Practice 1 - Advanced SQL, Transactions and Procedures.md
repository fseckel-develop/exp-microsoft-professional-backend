## Step 1: Configure the Database Structure

```sql
CREATE DATABASE StoreDB;
USE StoreDB;
```

```sql
CREATE TABLE Products (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    ProductName VARCHAR(50),
    Brand VARCHAR(50),
    Category VARCHAR(50),
    Price DECIMAL(10,2),
    DateAdded DATE
);
```

---
## Step 2: Inserting Data into the Users Table

```sql
INSERT INTO Products 
	(ProductName, Brand, Category, Price, DateAdded) 
VALUES
	('Laptop X1', 'TechPro', 'Electronics', 1200.00, '2020-03-15'),
	('Smartphone A5', 'MobileMax', 'Electronics', 800.00, '2019-03-15'),
	('Office Chair', 'ComfortLine', 'Furniture', 150.00, '2020-07-22'),
	('Desk Lamp', 'BrightHome', 'Furniture', 45.00, '2018-10-05'),
	('Running Shoes', 'FastTrack', 'Sportswear', 120.00, '2018-03-29');
```

---
## Step 3: Managing Transactions and Concurrency

```sql
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
```

```sql
START TRANSACTION;

UPDATE Products
	SET Price = Price - 20
	WHERE Category = 'Furniture';
	
UPDATE Products
	SET Price = Price + 20
	WHERE Category = 'Electronics';

COMMIT;
```

```sql
UNLOCK TABLES;
```

---
## Step 4: Advanced SQL Query

```sql
SELECT ProductName, Brand, Price
	FROM Products
	WHERE Price > (
		SELECT AVG(Price) 
		FROM Products
	);
```

---
## Step 5: Creating a Common Table Expression (CTE)

```sql
WITH CategoryPricing AS (
	SELECT Category, 
		SUM(Price) AS TotalPrice
	 FROM Products
	 GROUP BY Category
)
SELECT * FROM CategoryPricing;
```

---
## Step 6: Creating a Stored Procedures

#### Example 1:

```sql
DELIMITER $$

CREATE PROCEDURE AdjustPrice(
	CategoryName VARCHAR(50), 
	AdjustmentAmount DECIMAL(10,2)
)
BEGIN
	UPDATE Products
		SET Price = Price + AdjustmentAmount
		WHERE Category = CategoryName;
END $$ 
DELIMITER;
```

#### Example 2:

```sql
DELIMITER $$

CREATE PROCEDURE IncreasePrice (
    IN categoryName VARCHAR(50),
    IN increment DECIMAL(10, 2)
)
BEGIN
    IF increment <= 0 THEN
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Increment must be positive';
    END IF;
	
    UPDATE Products
	    SET Price = Price + increment
	    WHERE Category = categoryName;
	
    IF ROW_COUNT() = 0 THEN
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Category not found';
    END IF;
    
END $$ 
DELIMITER ;
```

Calling the Stored Procedure:

```sql
CALL IncreasePrice('Electronics', 50); 
SELECT * FROM Products;
```

---
## Step 7: Creating a SQL Function

#### Example 1:

```sql
DELIMITER $$

CREATE FUNCTION CalculateDiscount(
	PriceValue DECIMAL(10, 2)
)
	RETURNS DECIMAL(10, 2)
	DETERMINISTIC
BEGIN
	RETURN PriceValue * 0.10;
	
END $$
DELIMITER ;
```

#### Example 2:

```sql
DELIMITER $$

CREATE FUNCTION CalculateDiscount (
	price DECIMAL(10, 2)
)
	RETURNS DECIMAL(10, 2)
	DETERMINISTIC
BEGIN
    IF price <= 0 THEN
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Price must be positive';
    END IF;
    RETURN price * 0.10;
    
END $$
DELIMITER ;
```

Calling the SQL Function:

```sql
SELECT ProductName, Brand, 
	CalculateDiscount(Price) AS Discount 
	FROM Products;
```