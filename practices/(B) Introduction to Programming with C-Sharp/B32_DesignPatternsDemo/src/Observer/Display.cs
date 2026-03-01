namespace DesignPatternsDemo.Observer;

public abstract class Display : IObserver
{
    public abstract string Name { get; }
    public abstract void Update(float temperature);
}