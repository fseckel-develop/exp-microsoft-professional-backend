public class Book {
    public string Title { get; set; }
    public bool IsBorrowed { get; set; } = false;

    public Book(string title) {
        Title = title;
        IsBorrowed = false;
    }
}

public class Library {
    private List<Book> books = new List<Book>();
    private int capacity;
    private int borrowedBooks = 0;

    public Library(int capacity) {
        this.capacity = capacity;
    }

    public void AddBook() {
        if (books.Count >= capacity) {
            Console.WriteLine("\nLibrary is at full capacity. Cannot add more books.");
            return;
        }
        Console.Write("\nEnter book title: ");
        var title = Console.ReadLine();
        books.Add(new Book(title!));
        Console.WriteLine("Book was added: " + title);
    }

    public void DeleteBook() {
        Console.Write("\nEnter book title to delete: ");
        var title = Console.ReadLine();
        foreach (var book in books) {
            if (book.Title.Equals(title)) {
                books.Remove(book);
                Console.WriteLine("Book was removed: " + book);
                return;
            }
        }
        Console.WriteLine($"Book \"{title}\" not found in the library.");
    }

    public void SearchBook() {
        Console.Write("\nEnter book title to search: ");
        var title = Console.ReadLine();
        foreach (var book in books) {
            if (book.Title.Equals(title)) {
                Console.WriteLine("The book is available.");
                return;
            }
        }
        Console.WriteLine($"Book \"{title}\" not found in the library.");
    }

    public void BorrowBook() {
        if (borrowedBooks >= 3) {
            Console.WriteLine("\nYou have reached the maximum number of borrowed books.");
            return;
        }
        Console.Write("\nEnter book title to borrow: ");
        var title = Console.ReadLine();
        foreach (var book in books) {
            if (book.Title.Equals(title) && !book.IsBorrowed) {
                book.IsBorrowed = true;
                borrowedBooks++;
                Console.WriteLine("You have borrowed: " + title);
                return;
            }
        }
        Console.WriteLine("Book not available for borrowing.");
    }

    public void ReturnBook() {
        Console.Write("\nEnter book title to return: ");
        var title = Console.ReadLine();
        foreach (var book in books) {
            if (book.Title.Equals(title) && book.IsBorrowed) {
                book.IsBorrowed = false;
                borrowedBooks--;
                Console.WriteLine("You have returned: " + title);
                return;
            }
        }
        Console.WriteLine("This book was not borrowed from this library.");
    }

    public void ListBooks() {
        Console.WriteLine("\nBooks in Library:");
        foreach (var book in books) {
            Console.Write(" - " + book);
            if (book.IsBorrowed) Console.Write(" (Borrowed)");
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Library library = new Library(5);

        while (true) {
            Console.WriteLine();
            Console.WriteLine("(A) Add Book");
            Console.WriteLine("(D) Delete Book");
            Console.WriteLine("(S) Search Book");
            Console.WriteLine("(B) Borrow Book");
            Console.WriteLine("(R) Return Book");
            Console.WriteLine("(L) List Books");
            Console.WriteLine("(X) Exit");
            Console.Write("Choose an option: ");
            var choice = Console.ReadLine();

            switch (choice) {
                case "A":
                    library.AddBook();
                    break;
                case "D":
                    library.DeleteBook();
                    break;
                case "S":
                    library.SearchBook();
                    break;
                case "B":
                    library.BorrowBook();
                    break;
                case "R":
                    library.ReturnBook();
                    break;
                case "L":
                    library.ListBooks();
                    break;
                case "X":
                    return;
                default:
                    Console.WriteLine("\nInvalid option. Please try again.");
                    break;
            }
        }
    }
}
