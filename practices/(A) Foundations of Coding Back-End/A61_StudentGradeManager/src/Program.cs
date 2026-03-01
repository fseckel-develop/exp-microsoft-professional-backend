using StudentGradeManager.Presentation;
using StudentGradeManager.Services;

namespace StudentGradeManager;

internal static class Program
{
    private static void Main()
    {
        var service = new GradeManagementService();
        var writer = new ConsoleWriter();
        var menu = new ConsoleMenu(service, writer);

        menu.Run();
    }
}