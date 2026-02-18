public class Task
{
    public string Name { get; }
    public int Start { get; }
    public int End { get; }
    public int Value { get; }

    public Task(string name, int start, int end, int value)
    {
        Name = name;
        Start = start;
        End = end;
        Value = value;
    }
}