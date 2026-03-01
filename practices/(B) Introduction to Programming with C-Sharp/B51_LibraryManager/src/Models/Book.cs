namespace LibraryManager.Models;

public sealed class Book
{
    public int Id { get; }
    public string Title { get; }
    public bool IsBorrowed { get; private set; }

    public Book(int id, string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Book title is required.", nameof(title));

        Id = id;
        Title = title.Trim();
    }

    public bool Borrow()
    {
        if (IsBorrowed)
            return false;

        IsBorrowed = true;
        return true;
    }

    public bool Return()
    {
        if (!IsBorrowed)
            return false;

        IsBorrowed = false;
        return true;
    }

    public override string ToString()
        => $"[{Id}] {Title}";
}