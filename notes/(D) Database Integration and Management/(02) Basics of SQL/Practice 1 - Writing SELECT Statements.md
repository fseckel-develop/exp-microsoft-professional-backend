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
## Step 2: Writing SELECT Statements

(1) Retrieve all columns: 

```sql
SELECT * FROM Employees;
```


(2) Retrieve specific columns: 

```sql
SELECT FirstName, LastName FROM Employees;
```


(3) Retrieve unique departments: 

```sql
SELECT DISTINCT Department FROM Employees;
```


---
## Step 3: Using the WHERE Clause

(1) Employees in HR: 

```sql
SELECT * FROM Employees WHERE Department = 'HR';
``` 


(2) Finance employees with salary > 60,000: 

```sql
SELECT * FROM Employees WHERE Department = 'Finance' AND Salary > 60000;
```


(3) Employees with >5 years and salary <70,000: 

```sql
SELECT * FROM Employees WHERE YearsExperience > 5 AND Salary < 70000;
```


---
## Step 4: Sorting with the ORDER BY Clause

(1) Sort by LastName: 

```sql
SELECT * FROM Employees ORDER BY LastName ASC;
```


(2) HR employees sorted by salary descending: 

```sql
SELECT * FROM Employees WHERE Department = 'HR' ORDER BY Salary DESC;
```


(3) Top 3 earners: 

```sql
SELECT * FROM Employees ORDER BY Salary DESC LIMIT 3;
```


---
## Step 5: More complex Queries

(1) IT employees sorted by experience: 

```sql
SELECT * FROM 
	Employees 
	WHERE 
		Department = 'IT' AND YearsExperience > 3 
	ORDER BY 
		YearsExperience DESC;
```


(2) Salary between 50.000 and 75.000 sorted by first name: 

```sql
SELECT * FROM 
	Employees 
	WHERE 
		Salary BETWEEN 50000 AND 75000 
	ORDER BY 
		FirstName ASC;
```

---
## Step 6: Aggregate Queries

(1) Total Salaries by Department via SQL Aggregate Query:

```sql
SELECT Department, 
	SUM(Salary) AS TotalSalary 
	FROM Employees 
	GROUP BY Department;
```


(2) Employee Count by Department via SQL Aggregate Query:

```sql
SELECT Department, 
	COUNT(*) AS EmployeeCount 
	FROM Employees 
	GROUP BY Department;
```