## Step 1: Prepare for the Lab Environment

```sql
CREATE DATABASE ClinicDB;
USE ClinicDB;

CREATE TABLE Appointments (
    AppointmentID INT AUTO_INCREMENT PRIMARY KEY,
    AppointmentDate DATETIME,
    PatientID INT,
    ConsultationFee DECIMAL(10, 2)
);

DELIMITER $$

CREATE PROCEDURE PopulateAppointments()
BEGIN
    DECLARE i INT DEFAULT 1;
    WHILE i <= 3000 DO
        INSERT INTO Appointments (AppointmentDate, PatientID, ConsultationFee)
        VALUES (
            DATE_ADD('2024-01-01', INTERVAL FLOOR(RAND() * 90) DAY), 
            -- Random date in Quarter 1 2024
            FLOOR(RAND() * 100) + 1, -- Random PatientID between 1 and 100
            ROUND(RAND() * 1000, 2)  -- Random ConsultationFee between 0 and 1000
        );
        SET i = i + 1;
    END WHILE;
END $$

DELIMITER ;

CALL PopulateAppointments();
```

To confirm that the data has been inserted

```sql
SELECT COUNT(*) AS TotalRows FROM Appointments;
```

---
## Step 2: Measure Baseline Query Performance

Enable query timing by running:

```sql
SET PROFILING = 1;
```

Execute the following query and measure its execution time

```sql
SELECT * FROM Appointments  
	WHERE AppointmentDate BETWEEN '2024-02-01' AND '2024-02-15'
	AND ConsultationFee > 500;
```

View the profiling information by running:

```sql
SHOW PROFILES;
```

---
## Step 3: Create Indexes

Create a single-column index on the AppointmentDate column:

```sql
CREATE INDEX idx_appointment_date ON Appointments(AppointmentDate);
```

Create another single-column index on the PatientID column:

```sql
CREATE INDEX idx_patient_id ON Appointments(PatientID);
```

Verify the indexes were created by running:

```sql
SHOW INDEX FROM Appointments;
```

---
## Step 4: Measure Query Performance After Indexing

Rerun the same query from Step 2:

```sql
SELECT * FROM Appointments  
	WHERE AppointmentDate BETWEEN '2024-02-01' AND '2024-02-15'  
	AND ConsultationFee > 500;
```

View the profiling information again using:

```sql
SHOW PROFILES;
```

Note the updated execution time and compare it to the baseline.
It should show some improvement.

---
## Step 5: Analyse Query Execution Plans

Use the EXPLAIN command to analyse the query:

```sql
EXPLAIN SELECT * FROM Appointments  
	WHERE AppointmentDate BETWEEN '2024-02-01' AND '2024-02-15'  
	AND ConsultationFee > 500;
```

---
## Step 6: Experiment with Composite Indexes

Drop the existing single-column index on PatientID:

```sql
DROP INDEX idx_patient_id ON Appointments;
```

Create a composite index on AppointmentDate and ConsultationFee:

```sql
CREATE INDEX idx_appointment_date_fee ON Appointments(AppointmentDate, ConsultationFee);
```

Rerun the query from Step 2 and measure the execution time again:

```sql
SELECT * FROM Appointments  
	WHERE AppointmentDate BETWEEN '2024-02-01' AND '2024-02-15'  
	AND ConsultationFee > 500;
```

Use the EXPLAIN command to confirm that the composite index is being used:

```sql
EXPLAIN SELECT * FROM Appointments  
	WHERE AppointmentDate BETWEEN '2024-02-01' AND '2024-02-15'  
	AND ConsultationFee > 500;
```