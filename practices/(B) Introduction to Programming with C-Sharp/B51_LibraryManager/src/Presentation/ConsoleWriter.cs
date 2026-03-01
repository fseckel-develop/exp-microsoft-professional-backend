using LibraryManager.Models;

namespace LibraryManager.Presentation;

public sealed class ConsoleWriter
{
    public void WriteMenu()
    {
        Console.WriteLine();
        Console.WriteLine("(A) Add Book");
        Console.WriteLine("(D) Delete Book");
        Console.WriteLine("(S) Search Book");
        Console.WriteLine("(B) Borrow Book");
        Console.WriteLine("(R) Return Book");
        Console.WriteLine("(L) List Books");
        Console.WriteLine("(X) Exit");
        Console.Write("Choose an option: ");
    }

    public void WriteMessage(string message)
    {
        Console.WriteLine();
        Console.WriteLine(message);
    }

    public void WriteBooks(IEnumerable<Book> books)
    {
        Console.WriteLine();
        Console.WriteLine("Books in Library:");

        foreach (var book in books)
        {
            Console.WriteLine(
                $" {book.Id,3} | {book.Title,-30} | {(book.IsBorrowed ? "Borrowed" : "Available")}");
        }
    }
}