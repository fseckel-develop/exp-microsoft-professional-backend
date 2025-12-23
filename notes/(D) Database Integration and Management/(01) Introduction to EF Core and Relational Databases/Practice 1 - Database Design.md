## Example 1: Bookstore Database

- **Problem Definition**:
	
	- Design a database to manage information about books, authors, and publishers for a bookstore.
	- The system must store book details, author information, publisher data, and the relationships between them.
	- The database should support books with multiple authors and publishers that publish multiple books.

- **Key Entities (Tables)**:
	
	- Books
	- Authors
	- Publishers
	- BookAuthors

- **Table Structures (Attributes)**:
	
	- **Books Table**:
		- BookID (PK)
		- Title
		- ISBN (UNIQUE)
		- PublisherID (FK)
	
	- **Authors Table**:
		- AuthorID (PK)
		- FirstName
		- LastName
	
	- **Publishers Table**:
		- PublisherID (PK)
		- Name
		- Address
	
	- **Junction Table (BookAuthors)**:
		- BookID (FK)
		- AuthorID (FK)

- **Relationships Between Tables**:
	
	- **One-to-Many**:
		- One publisher can publish many books.
		- Each book is published by one publisher.
	
	- **Many-to-Many**:
		- A book can have multiple authors.
		- An author can write multiple books.
		- The BookAuthors table manages this relationship.

- **Keys and Constraints**:
	
	- **Primary Keys**:
		- BookID uniquely identifies each book.
		- AuthorID uniquely identifies each author.
		- PublisherID uniquely identifies each publisher.
	
	- **Foreign Keys**:
		- Books.PublisherID references Publishers.PublisherID.
		- BookAuthors.BookID references Books.BookID.
		- BookAuthors.AuthorID references Authors.AuthorID.
	
	- **Constraints**:
		- ISBN is UNIQUE to prevent duplicate book entries.
		- Primary keys are NOT NULL by default.

- **Schema Diagram (Logical View)**:
	
	- Books
		- BookID (PK)
		- Title
		- ISBN
		- PublisherID (FK)
	
	- Authors
		- AuthorID (PK)
		- FirstName
		- LastName
	
	- Publishers
		- PublisherID (PK)
		- Name
		- Address
	
	- BookAuthors
		- BookID (FK)
		- AuthorID (FK)

- **Design Summary**
	
	- This design organises bookstore data into clearly defined tables.
	- Relationships ensure efficient data retrieval while minimising redundancy.
	- The use of primary keys, foreign keys, and constraints maintains data integrity 
	  and supports scalability as the bookstore grows.

---
## Example 2: Movie Streaming Database

- **Problem Definition**:
	
	- Design a database for a movie streaming service to store information about movies, directors, and actors.
	- The system must manage movie details, director information, actor data, and the relationships between them.
	- The database should support movies with multiple actors and directors who can direct multiple movies.

- **Key Entities (Tables)**:
	
	- Movies
	- Directors
	- Actors
	- MovieActors

- **Table Structures (Attributes)**:
	
	- **Movies Table**:
		- MovieID (PK)
		- Title
		- ReleaseYear
		- DirectorID (FK)
	
	- **Directors Table**:
		- DirectorID (PK)
		- Name
	
	- **Actors Table**:
		- ActorID (PK)
		- Name
	
	- **Junction Table (MovieActors)**:
		- MovieID (FK)
		- ActorID (FK)

- **Relationships Between Tables**:
	
	- **One-to-Many**:
		- One director can direct many movies.
		- Each movie is directed by one director.
	
	- **Many-to-Many**:
		- A movie can feature multiple actors.
		- An actor can appear in multiple movies.
		- The MovieActors table manages this relationship.

- **Keys and Constraints**:
	
	- **Primary Keys**:
		- MovieID uniquely identifies each movie.
		- DirectorID uniquely identifies each director.
		- ActorID uniquely identifies each actor.
	
	- **Foreign Keys**:
		- Movies.DirectorID references Directors.DirectorID.
		- MovieActors.MovieID references Movies.MovieID.
		- MovieActors.ActorID references Actors.ActorID.
	
	- **Constraints**:
		- Primary keys are NOT NULL by default.
		- ReleaseYear must contain valid numeric values.

- **Schema Diagram (Logical View)**:
	
	- Movies
		- MovieID (PK)
		- Title
		- ReleaseYear
		- DirectorID (FK)
	
	- Directors
		- DirectorID (PK)
		- Name
	
	- Actors
		- ActorID (PK)
		- Name
	
	- MovieActors
		- MovieID (FK)
		- ActorID (FK)

- **Design Summary**
	
	- This design organizes movie streaming data into clearly defined tables.
	- Relationships support efficient querying of movies, directors, and actors.
	- The use of primary keys, foreign keys, and constraints ensures data integrity 
	  and supports scalability as the platform grows.

---
## Example 3: University Database

- **Problem Definition**:
	
	- Design a database to manage information about students, courses, and professors in a university.
	- The system must store student records, course details, professor information, and the relationships between them.
	- The database should support student enrollments in multiple courses and professors teaching multiple courses.

- **Key Entities (Tables)**:
	
	- Students
	- Courses
	- Professors
	- Enrollments

- **Table Structures (Attributes)**:
	
	- **Students Table**:
		- StudentID (PK)
		- FirstName
		- LastName
		- Email
	
	- **Professors Table**:
		- ProfessorID (PK)
		- FirstName
		- LastName
		- Department
	
	- **Courses Table**:
		- CourseID (PK)
		- CourseName
		- Credits
		- ProfessorID (FK)
	
	- **Junction Table (Enrollments)**:
		- StudentID (FK)
		- CourseID (FK)

- **Relationships Between Tables**:
	
	- **One-to-Many**:
		- One professor can teach many courses.
		- Each course is taught by one professor.
	
	- **Many-to-Many**:
		- A student can enroll in many courses.
		- A course can have many students.
		- The Enrollments table manages this relationship.

- **Keys and Constraints**:
	
	- **Primary Keys**:
		- StudentID uniquely identifies each student.
		- ProfessorID uniquely identifies each professor.
		- CourseID uniquely identifies each course.
	
	- **Foreign Keys**:
		- Courses.ProfessorID references Professors.ProfessorID.
		- Enrolments.StudentID references Students.StudentID.
		- Enrolments.CourseID references Courses.CourseID.
	
	- **Constraints**:
		- Email is UNIQUE for each student.
		- Primary keys are NOT NULL by default.

- **Schema Diagram (Logical View)**:
	
	- Students
		- StudentID (PK)
		- FirstName
		- LastName
		- Email
	
	- Professors
		- ProfessorID (PK)
		- FirstName
		- LastName
		- Department
	
	- Courses
		- CourseID (PK)
		- CourseName
		- Credits
		- ProfessorID (FK)
	
	- Enrolments
		- StudentID (FK)
		- CourseID (FK)

- **Design Summary**
	
	- This design organises university data into clearly structured tables.
	- Relationships allow efficient tracking of students, courses, and professors.
	- Primary keys, foreign keys, and constraints ensure data integrity 
	  and support scalability as the university system grows.

---
## Example 4: Library Database

- **Problem Definition**:
	
	- Design a database to manage information about members, books, and loans in a library.
	- The system must store member details, book information, and loan records.
	- The database should support tracking which members borrow which books and when.

- **Key Entities (Tables)**:
	
	- Members
	- Books
	- Loans

- **Table Structures (Attributes)**:
	
	- **Members Table**:
		- MemberID (PK)
		- FirstName
		- LastName
		- Email
	
	- **Books Table**:
		- BookID (PK)
		- Title
		- ISBN (UNIQUE)
		- Author
	
	- **Loans Table**:
		- LoanID (PK)
		- MemberID (FK)
		- BookID (FK)
		- LoanDate
		- DueDate
		- ReturnDate

- **Relationships Between Tables**:
	
	- **One-to-Many**:
		- One member can have many loans.
		- Each loan is associated with one member.
	
	- **Many-to-Many**:
		- A book can be borrowed many times by different members.
		- A member can borrow many books over time.
		- The Loans table manages this relationship.

- **Keys and Constraints**:
	
	- **Primary Keys**:
		- MemberID uniquely identifies each member.
		- BookID uniquely identifies each book.
		- LoanID uniquely identifies each loan.
	
	- **Foreign Keys**:
		- Loans.MemberID references Members.MemberID.
		- Loans.BookID references Books.BookID.
	
	- **Constraints**:
		- Email is UNIQUE for each member.
		- ISBN is UNIQUE for each book.
		- LoanDate and DueDate are NOT NULL.
		- Primary keys are NOT NULL by default.

- **Schema Diagram (Logical View)**:
	
	- Members
		- MemberID (PK)
		- FirstName
		- LastName
		- Email
	
	- Books
		- BookID (PK)
		- Title
		- ISBN
		- Author
	
	- Loans
		- LoanID (PK)
		- MemberID (FK)
		- BookID (FK)
		- LoanDate
		- DueDate
		- ReturnDate

- **Design Summary**
	
	- This design structures library data into logical, well-defined tables.
	- Relationships allow accurate tracking of book loans and borrowing history.
	- The use of keys and constraints ensures data integrity 
	  and supports efficient, scalable library operations.