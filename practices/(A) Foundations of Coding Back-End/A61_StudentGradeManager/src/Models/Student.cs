namespace StudentGradeManager.Models;

public sealed class Student
{
    private readonly Dictionary<string, double> _grades = new(StringComparer.OrdinalIgnoreCase);

    public int Id { get; }
    public string Name { get; private set; }

    public IReadOnlyDictionary<string, double> Grades => _grades;

    public Student(int id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Student name is required.", nameof(name));

        Id = id;
        Name = name.Trim();
    }

    public void AddOrUpdateGrade(string subject, double grade)
    {
        if (string.IsNullOrWhiteSpace(subject))
            throw new ArgumentException("Subject is required.", nameof(subject));

        if (grade < 0 || grade > 100)
            throw new ArgumentOutOfRangeException(nameof(grade), "Grade must be between 0 and 100.");

        _grades[subject.Trim()] = grade;
    }

    public double GetAverageGrade()
    {
        return _grades.Count == 0
            ? 0.0
            : _grades.Values.Average();
    }
}