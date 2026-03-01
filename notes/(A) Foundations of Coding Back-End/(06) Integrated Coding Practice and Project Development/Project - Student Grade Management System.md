## Problem Statement:

To complete this challenge, you will need to create a console application to manage student grades. The system needs to be able to add students, add grades to different students, and calculate average grades for students. 

Some key features include:
- Add new students with names and IDs.
- Assign grades for different subjects.
- Calculate the average grade for each student.
- Display student records with their grades.

---
## Functional Requirements:

These describe the _core features_ the system needs to provide. (What the system must do)
  
- **Student Management**:
	- The system must allow users to add new students.
	- Each student must have:
		- Unique student ID
		- Student name
	
- **Grade Management**:
	- The system must allow users to assign grades to specific subjects for each student.
	- The system must allow updating or overwriting existing grades for a subject.
	- The system must store grades for multiple subjects per student.
    
- **Calculations**:
	- The system must calculate the **average grade** for each student.
	- The system must handle cases where no grades exist (average = 0 or “N/A”).
	
- **Display / Output**:
	- The system must display:
	    - Student details (name and ID)
	    - All subjects and grades for each student
	    - The average grade for each student
	- The system must show all stored student records upon request.
    
- **User Interaction**:
	- The system must provide a menu with options:
	    - Add Student
	    - Add Grade
	    - Display Students
	    - Exit Program

---
## Non-Functional Requirements:

These define qualities, constraints, and standards - not features. (How the system must behave)

- **Usability**:
	- The console interface must be simple and easy to navigate for all users.
	- Error messages must be clear (e.g., “Student not found,” “Invalid input”).
	
- **Reliability**:
	- The system must handle invalid input without crashing.
	- The program must continue running until the user chooses to exit.
    
- **Maintainability**:
	- The code must be written using clean structure:
	    - Student class
	    - Management/Controller class
	- Future developers should easily extend features (e.g., saving to files).
    
- **Performance**:
	- The system should process commands instantly.
	- Should handle a reasonable number of students (50–500) without slowing down.
	
- **Portability**:
	- The application must run on any system capable of running .NET/C# console applications (Windows, Linux, macOS).

---
## Project Objectives:

These describe the _specific goals the final system must achieve_.

1. **Build a fully functional console-based Student Grade Management System in C#.**
2. **Allow users to store and manage student records**, like IDs, names, subjects, and grades.
3. **Provide calculation capability** to compute the average grade for each student.
4. **Implement a menu-driven interface** to make the system easy to use.
5. **Demonstrate basic object-oriented programming principles**, including:
    - Classes
    - Encapsulation
    - Collections (e.g., List, Dictionary)
6. **Produce accurate and readable output** to sum up each student’s academic performance.

---
## Design Outline:

- **Classes with their Properties and Methods**:
	- class **Student class**:
		- Fields/Properties:
		    - string StudentID
		    - string Name
		    - Dictionary<string, double> Grades
		- Methods:
		    - void AddGrade(string subject, double grade)
		    - double CalculateAverage()
	
	- class **GradeManager**:
		- Fields/Properties:
		    - List< Student > Students
		- Methods:
		    - void AddStudent(string id, string name)
		    - Student? GetStudentById(string id)
		    - void AddGradeToStudent(string id, string subject, double grade)
		    - void DisplayStudentRecord(string id)
		    - void DisplayAllStudents()
    
	- class **Program**:
		- Providing the Main method as entry point into the application, responsible for displaying the applications menu in a while-loop, waiting for the users menu choice and react accordingly by either calling the suitable method of the GradeManager or breaking the main loop to stop the program

- **Variables**:
	- bool running = true; → controls main loop
	- string userChoice → menu selection
	- Input variables such as:
	    - string id, string name, string subject
	    - double grade

- **Data Structures**:
	- List< Student > → stores all students
	- Dictionary<string, double> in each Student → stores subject → grade

- **Control Structures and Loops**:
	- while loop → main menu
	- switch statement → menu selection
	- foreach loop → listing students or grades
	- if statements → input validation, checking student existence

---

![[project_flowchart_grade_management.png]]