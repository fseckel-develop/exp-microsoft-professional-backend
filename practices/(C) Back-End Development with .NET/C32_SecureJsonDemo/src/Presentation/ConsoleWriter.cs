using SecureJsonDemo.Models;

namespace SecureJsonDemo.Presentation;

public sealed class ConsoleWriter
{
    public void WriteTitle(string title)
    {
        Console.WriteLine(title);
        Console.WriteLine(new string('=', title.Length));
        Console.WriteLine();
    }

    public void WriteSection(string title)
    {
        Console.WriteLine(title);
        Console.WriteLine(new string('-', title.Length));
    }

    public void WriteSerializedPackage(string serialized)
    {
        WriteSection("Serialized Package");
        Console.WriteLine(serialized);
        Console.WriteLine();
    }

    public void WriteRestoredPackage(DocumentPackage package)
    {
        WriteSection("Restored Package");
        Console.WriteLine($"DocumentName: {package.DocumentName}");
        Console.WriteLine($"Recipient:    {package.Recipient}");
        Console.WriteLine($"Contents:     {package.Contents}");
        Console.WriteLine();
    }

    public void WriteBlockedResult(bool blocked)
    {
        WriteSection("Untrusted Source Check");
        Console.WriteLine($"Result: {(blocked ? "Blocked" : "Allowed")}");
        Console.WriteLine();
    }

    public void WriteMessage(string message)
    {
        Console.WriteLine(message);
        Console.WriteLine();
    }
}