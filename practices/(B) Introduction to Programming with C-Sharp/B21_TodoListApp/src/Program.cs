using ToDoListApp.Presentation;
using ToDoListApp.Services;

namespace ToDoListApp;

internal static class Program
{
    private static void Main()
    {
        var service = new ToDoService(maxTasks: 10);
        var writer = new ConsoleWriter();
        var menu = new ConsoleMenu(service, writer);

        menu.Run();
    }
}