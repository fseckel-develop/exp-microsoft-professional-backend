using StudentGradeManager.Services;

namespace StudentGradeManager.Presentation;

public sealed class ConsoleMenu
{
    private readonly GradeManagementService _service;
    private readonly ConsoleWriter _writer;

    public ConsoleMenu(GradeManagementService service, ConsoleWriter writer)
    {
        _service = service;
        _writer = writer;
    }

    public void Run()
    {
        while (true)
        {
            _writer.WriteMenu();

            string choice = (Console.ReadLine() ?? string.Empty).Trim().ToUpperInvariant();
            Console.WriteLine();

            switch (choice)
            {
                case "S":
                    AddStudent();
                    break;
                case "G":
                    AddGrade();
                    break;
                case "D":
                    DisplayStudent();
                    break;
                case "L":
                    ListStudents();
                    break;
                case "X":
                    _writer.WriteMessage("Exiting the program. Goodbye!");
                    return;
                default:
                    _writer.WriteMessage("Invalid choice. Try again.");
                    break;
            }
        }
    }

    private void AddStudent()
    {
        Console.Write("Enter student name: ");
        string name = Console.ReadLine() ?? string.Empty;

        try
        {
            var student = _service.AddStudent(name);
            _writer.WriteMessage($"Student added successfully! Assigned ID: {student.Id}");
        }
        catch (ArgumentException ex)
        {
            _writer.WriteMessage(ex.Message);
        }
    }

    private void AddGrade()
    {
        Console.Write("Enter student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            _writer.WriteMessage("Invalid student ID.");
            return;
        }

        var student = _service.FindStudentById(id);
        if (student is null)
        {
            _writer.WriteMessage("Student not found.");
            return;
        }

        Console.Write("Enter subject: ");
        string subject = Console.ReadLine() ?? string.Empty;

        Console.Write("Enter grade:   ");
        if (!double.TryParse(Console.ReadLine(), out double grade))
        {
            _writer.WriteMessage("Invalid grade.");
            return;
        }

        try
        {
            student.AddOrUpdateGrade(subject, grade);
            _writer.WriteMessage("Grade added successfully!");
        }
        catch (Exception ex) when (ex is ArgumentException or ArgumentOutOfRangeException)
        {
            _writer.WriteMessage(ex.Message);
        }
    }

    private void DisplayStudent()
    {
        Console.Write("Enter student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            _writer.WriteMessage("Invalid student ID.");
            return;
        }

        var student = _service.FindStudentById(id);
        if (student is null)
        {
            _writer.WriteMessage("Student not found.");
            return;
        }

        _writer.WriteStudentRecord(student);
    }

    private void ListStudents()
    {
        var students = _service.GetAllStudents();

        if (students.Count == 0)
        {
            _writer.WriteMessage("No students in the system yet.");
            return;
        }

        _writer.WriteStudentList(students);
    }
}