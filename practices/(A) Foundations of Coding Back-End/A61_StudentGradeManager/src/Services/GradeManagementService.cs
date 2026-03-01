using StudentGradeManager.Models;

namespace StudentGradeManager.Services;

public sealed class GradeManagementService
{
    private readonly List<Student> _students = [];
    private int _nextStudentId = 1;

    public Student AddStudent(string name)
    {
        var student = new Student(_nextStudentId++, name);
        _students.Add(student);
        return student;
    }

    public bool TryAddGrade(int studentId, string subject, double grade)
    {
        var student = FindStudentById(studentId);
        if (student is null)
            return false;

        student.AddOrUpdateGrade(subject, grade);
        return true;
    }

    public Student? FindStudentById(int id)
    {
        return _students.FirstOrDefault(s => s.Id == id);
    }

    public IReadOnlyList<Student> GetAllStudents()
    {
        return _students
            .OrderBy(s => s.Id)
            .ToList();
    }

    public bool HasStudents()
    {
        return _students.Count > 0;
    }
}