public class Program
{
    public static void Main(string[] args)
    {
        List<Animal> animals = new List<Animal>();
        animals.Add(new Dog());
        animals.Add(new Cat());

        foreach (Animal animal in animals)
        {
            animal.Eat();
            animal.MakeSound();
        }
    }
}

public interface IAnimal
{
    void Eat();
}

public class Animal : IAnimal
{
    public virtual void Eat()
    {
        Console.WriteLine("Eating some animal food");
    }

    public virtual void MakeSound()
    {
        Console.WriteLine("Making some animal sound");
    }
}

public class Dog : Animal
{
    public override void Eat()
    {
        Console.WriteLine("Eating some Kibble");
    }

    public override void MakeSound()
    {
        Console.WriteLine("Bark! Bark!");
    }
}

public class Cat : Animal
{
    public override void Eat()
    {
        Console.WriteLine("Eating some Tuna");
    }

    public override void MakeSound()
    {
        Console.WriteLine("Meooow...");
    }
}
