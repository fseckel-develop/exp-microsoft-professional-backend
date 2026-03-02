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
## Step 2: Using SQL Functions for Data Manipulation

(1) Concatenate names: 

```sql
SELECT 
	CONCAT(FirstName, ' ', LastName) AS FullName 
	FROM Employees;
```

(2) Convert department to uppercase: 

```sql
SELECT 
	UPPER(Department) AS UpperDepartment 
	FROM Employees;
```

(3) Convert last name to lowercase: 

```sql
SELECT 
	LOWER(LastName) AS LowerLastName 
	FROM Employees;
```

(4) Calculate the length of first names: 

```sql
SELECT 
	LENGTH(FirstName) AS FirstNameLength 
	FROM Employees;
```

(5) Extract first three characters of last names: 

```sql
SELECT 
	SUBSTRING(LastName, 1, 3) AS LastNameSnippet 
	FROM Employees;
```


---
## Step 3: Using Aggregate Functions

(1) Count total employees: 

```sql
SELECT 
	COUNT( * ) AS TotalEmployees 
	FROM Employees;
```

(2) Calculate total salary: 

```sql
SELECT 
	SUM(Salary) AS TotalSalary
	FROM Employees;
```

(3) Average salary in Engineering: 

```sql
SELECT 
	AVG(Salary) AS AvgEngineeringSalary 
	FROM Employees 
	WHERE Department = 'Engineering';
```

(4) Minimum salary: 

```sql
SELECT 
	MIN(Salary) AS MinSalary 
	FROM Employees;
```

(5) Maximum salary in Sales: 

```sql
SELECT 
	MAX(Salary) AS MaxSalesSalary 
	FROM Employees 
	WHERE Department = 'Sales';
```


---
## Step 4: Combining Aggregate Functions with GROUP BY

(1) Total salary by department: 

```sql
SELECT Department, 
	SUM(Salary) AS TotalSalary 
	FROM Employees 
	GROUP BY Department;
```

(2) Average salary by department: 

```sql
SELECT Department, 
	AVG(Salary) AS AvgSalary 
	FROM Employees 
	GROUP BY Department;
```

(3) Count employees by department: 

```sql
SELECT Department, 
	COUNT( * ) AS EmployeeCount 
	FROM Employees 
	GROUP BY Department;
```


---
## Step 5: Exploring Advanced Functions

(1) Length of concatenated names: 

```sql
SELECT 
	CONCAT(FirstName, ' ', LastName) AS FullName, 
	LENGTH(CONCAT(FirstName, ' ', LastName)) AS FullNameLength 
	FROM Employees;
```

(2) Count employees by year of hiring: 

```sql
SELECT 
	YEAR(HireDate) AS HireYear, 
	COUNT( * ) AS EmployeeCount 
	FROM Employees 
	GROUP BY HireYear;
```

(3) Total salary by year of hiring: 

```sql
SELECT 
	YEAR(HireDate) AS HireYear, 
	SUM(Salary) AS TotalSalary 
	FROM Employees 
	GROUP BY HireYear;
```

## Step 2: Using SQL Functions for Data Manipulation

(1) Concatenate product name and brand:

```sql
SELECT 
	CONCAT(ProductName, ' - ', Brand) AS ProductLabel 
	FROM Products;
```

(2) Convert category to uppercase:

```sql
SELECT 
	UPPER(Category) AS UpperCategory 
	FROM Products;
```

(3) Convert brand to lowercase:

```sql
SELECT 
	LOWER(Brand) AS LowerBrand 
	FROM Products;
```

(4) Calculate the length of product names:

```sql
SELECT 
	LENGTH(ProductName) AS ProductNameLength 
	FROM Products;
```

(5) Extract first three characters of product names:

```sql
SELECT 
	SUBSTRING(ProductName, 1, 3) AS ProductSnippet 
	FROM Products;
```

---
## Step 3: Using Aggregate Functions

(1) Count total products:

```sql
SELECT 
	COUNT(*) AS TotalProducts 
	FROM Products;
```

(2) Calculate total inventory value:

```sql
SELECT 
	SUM(Price * StockQuantity) AS TotalInventoryValue
	FROM Products;
```

(3) Average price in Electronics:

```sql
SELECT 
	AVG(Price) AS AvgElectronicsPrice 
	FROM Products 
	WHERE Category = 'Electronics';
```

(4) Minimum price:

```sql
SELECT 
	MIN(Price) AS MinPrice 
	FROM Products;
```

(5) Maximum price in Furniture:

```sql
SELECT 
	MAX(Price) AS MaxFurniturePrice 
	FROM Products 
	WHERE Category = 'Furniture';
```

---
## Step 4: Combining Aggregate Functions with GROUP BY

(1) Total inventory value by category:

```sql
SELECT Category, 
	SUM(Price * StockQuantity) AS TotalInventoryValue 
	FROM Products 
	GROUP BY Category;
```

(2) Average price by category:

```sql
SELECT Category, 
	AVG(Price) AS AvgPrice 
	FROM Products 
	GROUP BY Category;
```

(3) Count products by category:

```sql
SELECT Category, 
	COUNT(*) AS ProductCount 
	FROM Products 
	GROUP BY Category;
```

---
## Step 5: Exploring Advanced Functions

(1) Length of concatenated product labels:

```sql
SELECT 
	CONCAT(ProductName, ' - ', Brand) AS ProductLabel, 
	LENGTH(CONCAT(ProductName, ' - ', Brand)) AS ProductLabelLength 
	FROM Products;
```

(2) Count products by year added:

```sql
SELECT 
	YEAR(DateAdded) AS AddedYear, 
	COUNT(*) AS ProductCount 
	FROM Products 
	GROUP BY AddedYear;
```

(3) Total inventory value by year added:

```sql
SELECT 
	YEAR(DateAdded) AS AddedYear, 
	SUM(Price * StockQuantity) AS TotalInventoryValue 
	FROM Products 
	GROUP BY AddedYear;
```