using LibraryManager.Services;

namespace LibraryManager.Presentation;

public sealed class ConsoleMenu
{
    private readonly LibraryService _library;
    private readonly ConsoleWriter _writer;

    public ConsoleMenu(LibraryService library, ConsoleWriter writer)
    {
        _library = library;
        _writer = writer;
    }

    public void Run()
    {
        while (true)
        {
            _writer.WriteMenu();
            var choice = (Console.ReadLine() ?? string.Empty).Trim().ToUpperInvariant();

            switch (choice)
            {
                case "A":
                    AddBook();
                    break;
                case "D":
                    DeleteBook();
                    break;
                case "S":
                    SearchBook();
                    break;
                case "B":
                    BorrowBook();
                    break;
                case "R":
                    ReturnBook();
                    break;
                case "L":
                    ListBooks();
                    break;
                case "X":
                    _writer.WriteMessage("Goodbye!");
                    return;
                default:
                    _writer.WriteMessage("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private void AddBook()
    {
        Console.Write("\nEnter book title: ");
        var title = Console.ReadLine() ?? string.Empty;

        _library.AddBook(title, out var message);
        _writer.WriteMessage(message);
    }

    private void DeleteBook()
    {
        Console.Write("\nEnter book ID to delete: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            _writer.WriteMessage("Invalid ID.");
            return;
        }

        _library.DeleteBook(id, out var message);
        _writer.WriteMessage(message);
    }

    private void SearchBook()
    {
        Console.Write("\nEnter book ID to search: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            _writer.WriteMessage("Invalid ID.");
            return;
        }

        var book = _library.FindById(id);

        if (book is null)
        {
            _writer.WriteMessage($"Book with ID {id} not found.");
            return;
        }

        var availability = book.IsBorrowed ? "Borrowed" : "Available";
        _writer.WriteMessage($"Found: [{book.Id}] {book.Title} - {availability}");
    }

    private void BorrowBook()
    {
        Console.Write("\nEnter book ID to borrow: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            _writer.WriteMessage("Invalid ID.");
            return;
        }

        _library.BorrowBook(id, out var message);
        _writer.WriteMessage(message);
    }

    private void ReturnBook()
    {
        Console.Write("\nEnter book ID to return: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            _writer.WriteMessage("Invalid ID.");
            return;
        }

        _library.ReturnBook(id, out var message);
        _writer.WriteMessage(message);
    }

    private void ListBooks()
    {
        _writer.WriteBooks(_library.GetAllBooks());
    }
}