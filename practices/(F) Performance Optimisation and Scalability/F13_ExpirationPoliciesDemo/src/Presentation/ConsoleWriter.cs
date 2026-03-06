namespace ExpirationPoliciesDemo.Presentation;

public sealed class ConsoleWriter
{
    public void Title(string message)
    {
        Console.WriteLine(message);
        Console.WriteLine(new string('=', message.Length));
        Console.WriteLine();
    }

    public void Section(string message)
    {
        Console.WriteLine(message);
        Console.WriteLine(new string('-', message.Length));
    }

    public void Info(string message) => Console.WriteLine(message);

    public void Separator()
    {
        Console.WriteLine();
        Console.WriteLine(new string('-', 40));
        Console.WriteLine();
    }
}