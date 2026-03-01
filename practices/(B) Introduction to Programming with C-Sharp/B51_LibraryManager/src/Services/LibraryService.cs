using LibraryManager.Models;

namespace LibraryManager.Services;

public sealed class LibraryService
{
    private readonly List<Book> _books = [];
    private readonly int _capacity;
    private int _borrowedByCurrentUser;
    private int _nextBookId = 1;

    public LibraryService(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity));

        _capacity = capacity;
    }

    public IReadOnlyList<Book> GetAllBooks()
    {
        return _books
            .OrderBy(b => b.Id)
            .ToList();
    }

    public bool AddBook(string title, out string message)
    {
        if (_books.Count >= _capacity)
        {
            message = "Library is at full capacity.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            message = "Book title is required.";
            return false;
        }

        var book = new Book(_nextBookId++, title);
        _books.Add(book);

        message = $"Book added: [{book.Id}] {book.Title}";
        return true;
    }

    public bool DeleteBook(int id, out string message)
    {
        var book = FindById(id);

        if (book is null)
        {
            message = $"Book with ID {id} not found.";
            return false;
        }

        _books.Remove(book);
        message = $"Book removed: [{book.Id}] {book.Title}";
        return true;
    }

    public bool BorrowBook(int id, out string message)
    {
        if (_borrowedByCurrentUser >= 3)
        {
            message = "Maximum number of borrowed books reached.";
            return false;
        }

        var book = FindById(id);

        if (book is null)
        {
            message = $"Book with ID {id} not found.";
            return false;
        }

        if (!book.Borrow())
        {
            message = $"Book [{book.Id}] is already borrowed.";
            return false;
        }

        _borrowedByCurrentUser++;
        message = $"You borrowed [{book.Id}] {book.Title}";
        return true;
    }

    public bool ReturnBook(int id, out string message)
    {
        var book = FindById(id);

        if (book is null)
        {
            message = $"Book with ID {id} not found.";
            return false;
        }

        if (!book.Return())
        {
            message = $"Book [{book.Id}] was not borrowed.";
            return false;
        }

        if (_borrowedByCurrentUser > 0)
            _borrowedByCurrentUser--;

        message = $"You returned [{book.Id}] {book.Title}";
        return true;
    }

    public Book? FindById(int id)
    {
        return _books.FirstOrDefault(b => b.Id == id);
    }
}