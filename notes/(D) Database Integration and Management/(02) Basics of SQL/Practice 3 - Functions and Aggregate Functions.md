## Step 1: Configure the Database Structure

```sql
CREATE DATABASE EmployeeDB;
USE EmployeeDB;
```

```sql
CREATE TABLE Employees (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Department VARCHAR(50),
    Salary DECIMAL(10,2),
    YearsExperience INT
);
```

```sql
INSERT INTO Employees 
	(FirstName, LastName, Department, Salary, YearsExperience) 
VALUES
	('John', 'Doe', 'HR', 60000, 10),
	('Jane', 'Smith', 'Finance', 70000, 8),
	('Michael', 'Brown', 'IT', 50000, 5),
	('Emily', 'Davis', 'HR', 45000, 2),
	('Chris', 'Wilson', 'Finance', 80000, 15);
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