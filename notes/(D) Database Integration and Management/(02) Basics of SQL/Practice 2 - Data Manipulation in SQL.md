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
## Step 2: INSERT Operation

```sql
INSERT INTO Employees 
	(FirstName, LastName, Department, Salary, YearsExperience) 
VALUES 
	('Arjun', 'Patel', 'IT', 40000, 0);
```


---
## Step 3: UPDATE Operation

```sql
UPDATE Employees 
	SET Salary = 58000 
	WHERE FirstName = 'Michael' AND LastName = 'Brown';
```


---
## Step 4:  DELETE Operation

```sql
DELETE FROM Employees
	WHERE LastName = 'Wilson';
```


---
## Step 5: Safe SQL Practice

(1) Updating without WHERE clause:

```sql
UPDATE Employees 
	SET Salary = 75000;

-- Updates the Salary of every employee
```


(2) Undo changes: 

```sql
ROLLBACK;
```


(3) Updating specifically with WHERE clause:

```sql
UPDATE Employees 
	SET Salary 75000
	WHERE FirstName = 'Arjun';
```