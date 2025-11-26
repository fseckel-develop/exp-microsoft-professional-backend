class Student
{
    private static int nextID = 1;
    public string Name { get; set; }
    public int ID { get; set; }
    public Dictionary<string, double> Grades { get; set; }

    public Student(string name)
    {
        Name = name;
        ID = nextID++;
        Grades = new Dictionary<string, double>();
    }

    public void AddGrade(string subject, double grade)
    {
        Grades[subject] = grade;
    }

    public double GetAverageGrade()
    {
        if (Grades.Count == 0)
        {
            return 0.0;
        }
        double sum = 0;
        foreach (var grade in Grades.Values)
        {
            sum += grade;
        }
        return sum / Grades.Count;
    }

    public void PrintStudentInfo()
    {
        Console.WriteLine($"Student: {Name} (ID: {ID})");
    }

    public void PrintRecord()
    {
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine($"Student: {Name} (ID: {ID})");
        Console.WriteLine("Grades:");
        if (Grades.Count == 0)
        {
            Console.WriteLine("  No grades yet.");
        }
        else
        {
            foreach (var pair in Grades)
            {
                Console.WriteLine($"  {pair.Key}: {pair.Value}");
            }
        }
        Console.WriteLine($"Average Grade: {GetAverageGrade():0.00}");
        Console.WriteLine("--------------------------------------------\n");
    }
}

class GradeManagementSystem
{
    private List<Student> students = new List<Student>();

    private Student? FindStudentById(int id)
    {
        foreach (var student in students)
        {
            if (student.ID == id)
                return student;
        }
        return null;
    }

    public void AddStudent()
    {
        Console.Write("Enter student name: ");
        string name = Console.ReadLine()!;
        students.Add(new Student(name));
        Console.WriteLine("Student added successfully!\n");
    }

    public void AddGrade()
    {
        Console.Write("Enter student ID: ");
        int id = int.Parse(Console.ReadLine()!);

        Student? student = FindStudentById(id);
        if (student == null)
        {
            Console.WriteLine("Student not found.\n");
            return;
        }

        Console.Write("Enter subject: ");
        string subject = Console.ReadLine()!;

        Console.Write("Enter grade:   ");
        double grade = double.Parse(Console.ReadLine()!);

        student.AddGrade(subject, grade);
        Console.WriteLine("Grade added successfully!\n");
    }

    public void DisplayStudent()
    {
        Console.Write("Enter student ID: ");
        int id = int.Parse(Console.ReadLine()!);
        Student? student = FindStudentById(id);
        if (student == null)
        {
            Console.WriteLine("Student not found.\n");
            return;
        }
        student.PrintRecord();
    }

    public void DisplayAllStudents()
    {
        if (students.Count == 0)
        {
            Console.WriteLine("No students in the system yet.\n");
            return;
        }
        foreach (var student in students)
        {
            student.PrintStudentInfo();
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        GradeManagementSystem system = new GradeManagementSystem();

        while (true)
        {
            Console.WriteLine("====== Student Grade Management System ======");
            Console.WriteLine("  (S) Add Student");
            Console.WriteLine("  (G) Add Grade");
            Console.WriteLine("  (D) Display Student");
            Console.WriteLine("  (L) List Students");
            Console.WriteLine("  (X) Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine()!;
            Console.WriteLine();

            switch (choice)
            {
                case "S":
                    system.AddStudent();
                    break;
                case "G":
                    system.AddGrade();
                    break;
                case "D":
                    system.DisplayStudent();
                    break;
                case "L":
                    system.DisplayAllStudents();
                    break;
                case "X":
                    Console.WriteLine("Exiting the program. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.\n");
                    break;
            }
        }
    }
}
