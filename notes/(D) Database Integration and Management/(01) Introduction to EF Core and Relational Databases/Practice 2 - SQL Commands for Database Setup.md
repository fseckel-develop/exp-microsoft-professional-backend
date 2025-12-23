## Step 0: Start MySQL Database Management

```shell
mysql -u root -p
<then enter password>
```


---
## Step 1: Configure the Database Structure

```sql
CREATE DATABASE CompanyDB;
USE CompanyDB;
```

```sql
CREATE TABLE Departments (
	DepartmentID INT AUTO_INCREMENT PRIMARY KEY,
	DepartmentName VARCHAR(50) NOT NULL UNIQUE
);
```

```sql
CREATE TABLE Employees (
	EmployeeID INT AUTO_INCREMENT PRIMARY KEY,
	FirstName VARCHAR(50),
	Department VARCHAR(50)
);
```

```sql
ALTER TABLE Employees
	ADD COLUMN LastName VARCHAR(50) NOT NULL;
```

```sql
ALTER TABLE Employees
	ADD CONSTRAINT 
	FK_Department FOREIGN KEY (DepartmentID)
	REFERENCES Departments(DepartmentID);
```

```sql
DESCRIBE Departments;  -- Check Table Structure
```


---
## Step 2: Configure User Accounts and Permissions

```sql
CREATE USER 'manager'@'localhost' IDENTIFIED BY 'StrongPassword123';
GRANT ALL PRIVILEGES ON CompanyDB.* TO 'manager'@'localhost';
```


---
## Step 3: Add Data

```sql
INSERT INTO Departments (DepartmentName) 
	VALUES 
	('HR'), 
	('Engineering'), 
	('Marketing');
```

```sql
INSERT INTO Employees (FirstName, LastName, DepartmentID)
	VALUES
	('Margaret', 'Smith', 2),
	('Rejesh', 'Patel', 1),
	('Bjorn', 'Anderson', 3),
	('Gerry', 'Johnsen', 2),
	('Makaela', 'Lopez', 3),
	('Mary-Kate', 'Olsen', 1);
```


---
### Step 4: Test and Verify the Setup

```sql
SELECT * FROM Employees;  -- Shows all data entries
```