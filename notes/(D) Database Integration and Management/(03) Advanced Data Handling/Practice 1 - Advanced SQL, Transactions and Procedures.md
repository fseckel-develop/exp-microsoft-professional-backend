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
    HireDate DATA
);
```


---
## Step 2: Inserting Data into the Users Table

```sql
INSERT INTO Employees 
	(FirstName, LastName, Department, Salary, YearsExperience) 
VALUES
	('John', 'Doe', 'HR', 60000, '2020-03-15'),
	('Jane', 'Smith', 'Finance', 70000, '20219-03-15'),
	('Michael', 'Brown', 'IT', 50000, '2020-07-22'),
	('Emily', 'Davis', 'Marketing', 45000, '2018-10-05'),
	('Chris', 'Wilson', 'Finance', 80000, '2018-03-29');
```


---
## Step 3: Managing Transactions and Concurrency

```sql
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
```

```sql
START TRANSACTION;

UPDATE Employees
	SET Salary = Salary - 5000
	WHERE Department = 'Marketing';
	
UPDATE Employees
	SET Salary = Salary + 5000
	WHERE Department = 'Finance';

COMMIT;
```

```sql
UNLOCK TABLES;
```


---
## Step 4: Advanced SQL Query

```sql
SELECT FirstName, LastName, Salary
	FROM Employees
	WHERE Salary > (
		SELECT AVG(Salary) 
		FROM Employees
	);
```


---
## Step 5: Creating a Common Table Expression (CTE)

```sql
WITH DepartmentSalaries AS (
	SELECT Department, 
		SUM(Salary) AS TotalSalary
	 FROM Employees
	 GROUP BY Department
)
SELECT * FROM DepartmentSalaries;
```

---
## Step 6: Creating a Stored Procedures

#### Example 1:

```sql
DELIMITER $$

CREATE PROCEDURE AdjustSalary(
	DepartmentName VARCHAR(50), 
	AdjustmentAmount
)
BEGIN
	UPDATE Employees
		SET Salary = Salary + AdjustmentAmount
		WHERE Department = DepartmentName;
END $$ 
DELIMITER;
```

#### Example 2:

```sql
DELIMITER $$

CREATE PROCEDURE IncreaseSalary (
    IN deptName VARCHAR(50),
    IN increment DECIMAL(10, 2)
)
BEGIN
    IF increment <= 0 THEN
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Increment must be positive';
    END IF;
	
    UPDATE Employees
	    SET Salary = Salary + increment
	    WHERE Department = deptName;
	
    IF ROW_COUNT() = 0 THEN
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Department not found';
    END IF;
    
END $$ 
DELIMITER ;
```


Calling the Stored Procedure:

```sql
CALL IncreaseSalary('Finance', 5000); 
SELECT * FROM Employees;
```


---
## Step 7: Creating a SQL Function

#### Example 1:

```sql
DELIMITER  $ $

CREATE FUNCTION CalculateBonus(
	SalaryValue DECIMAL(10, 2)
)
	RETURNS DECIMAL(10, 2)
	DETERMINISTIC
BEGIN
	RETURN SalaryValue * 0.10;
	
END $$
DELIMITER;
```

#### Example 2:

```sql
DELIMITER $ $

CREATE FUNCTION CalculateBonus (
	salary DECIMAL(10, 2)
)
	RETURNS DECIMAL(10, 2)
	DETERMINISTIC
BEGIN
    IF salary <= 0 THEN
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Salary must be positive';
    END IF;
    RETURN salary * 0.10;
    
END $$
DELIMITER 
```


Calling the SQL Function:

```sql
SELECT FirstName, LastName, 
	CalculateBonus(Salary) AS Bonus 
	FROM Employees;
```

