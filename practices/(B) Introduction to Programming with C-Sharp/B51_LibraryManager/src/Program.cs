using LibraryManager.Presentation;
using LibraryManager.Services;

namespace LibraryManager;

internal static class Program
{
    private static void Main(string[] args)
    {
        var library = new LibraryService(capacity: 5);
        var writer = new ConsoleWriter();
        var menu = new ConsoleMenu(library, writer);

        menu.Run();
    }
}