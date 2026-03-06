## Step 1: Prepare for the Lab Environment

```sql
CREATE DATABASE UniversityDB;

USE UniversityDB;

CREATE TABLE Courses (
    course_id INT PRIMARY KEY AUTO_INCREMENT,
    course_name VARCHAR(100),
    credit_hours INT
);

CREATE TABLE Students (
    student_id INT PRIMARY KEY AUTO_INCREMENT,
    student_name VARCHAR(100)
);

CREATE TABLE Enrollments (
    enrollment_id INT PRIMARY KEY AUTO_INCREMENT,
    student_id INT,
    enrollment_date DATE,
    FOREIGN KEY (student_id) REFERENCES Students(student_id)
);

CREATE TABLE Grades (
    grade_id INT PRIMARY KEY AUTO_INCREMENT,
    course_id INT,
    score INT,
    FOREIGN KEY (course_id) REFERENCES Courses(course_id)
);
```

Verify the database setup:

```sql
SELECT * FROM Courses; 
SELECT * FROM Students; 
SELECT * FROM Enrollments; 
SELECT * FROM Grades;
```

---
## Step 2: Identifying Bottlenecks

Original Query:

```sql
SELECT * FROM Courses;
```

Analysis:

```sql
EXPLAIN SELECT * FROM Courses;

-- If type: ALL appears in the plan, it indicates a full table scan.
```

Optimised Query:

```sql
SELECT course_name, credit_hours FROM Courses WHERE credit_hours > 3;

-- Selecting specific columns reduces the amount of data processed.
```

---
## Step 3: Optimising Query Performance with Indexing

Original Query:

```sql
SELECT Enrollments.enrollment_id, Students.student_name 
FROM Enrollments  
JOIN Students ON Enrollments.student_id = Students.student_id;
```

Optimised Query:

```sql
CREATE INDEX idx_student_id ON Enrollments (student_id);

-- Adding an index on student_id speeds up row lookups.
```

---
## Step 4: Avoiding Inefficiencies in Temporary Tables

Original Query:

```sql
CREATE TEMPORARY TABLE temp_grades AS  
SELECT * FROM Grades WHERE score > 70;

SELECT AVG(score) FROM temp_grades;
```

Optimised Query:

```sql
SELECT AVG(score) FROM Grades WHERE score > 70;

-- Eliminating the temporary table reduces 
-- memory usage and improves execution speed.
```

---
## Step 5: Rewriting Nested Queries for Efficiency

Original Query:

```sql
SELECT * FROM Courses  
	WHERE course_id IN (
		SELECT course_id FROM Grades WHERE score > 70
	);
```

Optimised Query:

```sql
SELECT Courses.* 
FROM Courses  
JOIN Grades ON Courses.course_id = Grades.course_id  
WHERE Grades.score > 70;

-- JOIN queries are generally faster than subqueries 
-- as they reduce redundant executions
```