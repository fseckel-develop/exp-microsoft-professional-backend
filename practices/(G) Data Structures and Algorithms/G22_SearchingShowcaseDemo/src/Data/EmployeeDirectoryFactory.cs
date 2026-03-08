namespace SearchingAlgorithmsDemo.Data;

public static class EmployeeDirectoryFactory
{
    private static readonly string[] FirstNames =
    [
        "Alice", "Bob", "Carla", "Daniel", "Eva",
        "Farah", "Gavin", "Hannah", "Ivan", "Julia"
    ];

    private static readonly string[] LastNames =
    [
        "Meyer", "Schmidt", "Fischer", "Wagner", "Becker",
        "Hoffmann", "Schulz", "Klein", "Wolf", "Neumann"
    ];

    private static readonly string[] Departments =
    [
        "Finance", "Engineering", "Support", "HR", "Operations"
    ];

    public static EmployeeRecord[] CreateDirectory(int count, int seed = 42)
    {
        var random = new Random(seed);
        var employees = new EmployeeRecord[count];

        for (int i = 0; i < count; i++)
        {
            int employeeNumber = 100_000 + i;
            string name =
                $"{FirstNames[random.Next(FirstNames.Length)]} {LastNames[random.Next(LastNames.Length)]}";
            string department = Departments[random.Next(Departments.Length)];

            employees[i] = new EmployeeRecord(employeeNumber, name, department);
        }

        return employees;
    }

    public static EmployeeRecord[] Shuffle(EmployeeRecord[] source, int seed = 99)
    {
        var random = new Random(seed);
        var copy = (EmployeeRecord[])source.Clone();

        for (int i = copy.Length - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (copy[i], copy[j]) = (copy[j], copy[i]);
        }

        return copy;
    }
}