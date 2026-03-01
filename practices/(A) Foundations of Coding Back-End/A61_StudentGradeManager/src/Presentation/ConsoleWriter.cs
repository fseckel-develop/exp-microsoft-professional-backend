using StudentGradeManager.Models;

namespace StudentGradeManager.Presentation;

public sealed class ConsoleWriter
{
    public void WriteMenu()
    {
        Console.WriteLine("====== Student Grade Management System ======");
        Console.WriteLine("  (S) Add Student");
        Console.WriteLine("  (G) Add Grade");
        Console.WriteLine("  (D) Display Student");
        Console.WriteLine("  (L) List Students");
        Console.WriteLine("  (X) Exit");
        Console.Write("Select an option: ");
    }

    public void WriteMessage(string message)
    {
        Console.WriteLine(message);
        Console.WriteLine();
    }

    public void WriteStudentSummary(Student student)
    {
        Console.WriteLine($"Student #{student.Id,2}: {student.Name}");
    }

    public void WriteStudentRecord(Student student)
    {
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine($"Student: {student.Name} (ID: {student.Id})");
        Console.WriteLine("Grades:");

        if (student.Grades.Count == 0)
        {
            Console.WriteLine("  No grades yet.");
        }
        else
        {
            foreach (var grade in student.Grades.OrderBy(g => g.Key))
            {
                Console.WriteLine($"  {grade.Key,-15} {grade.Value,6:0.00}");
            }
        }

        Console.WriteLine($"Average Grade: {student.GetAverageGrade():0.00}");
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine();
    }

    public void WriteStudentList(IEnumerable<Student> students)
    {
        foreach (var student in students)
        {
            WriteStudentSummary(student);
        }

        Console.WriteLine();
    }
}