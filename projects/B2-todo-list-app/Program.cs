using System.Diagnostics;

public static class ToDoList
{
    private static String[] tasks = new String[10];
    private static int taskCount = 0;

    public static void AddTask()
    {
        if (taskCount == 10)
        {
            Console.WriteLine("Maximum number of tasks reached.\n");
            return;
        }
        Console.WriteLine("Enter the task to add: ");
        String? task = Console.ReadLine();
        if (task is not null && task != String.Empty)
        {
            tasks[taskCount++] = task;
        }
        else
        {
            Console.WriteLine("Invalid input!");
        }
        Console.WriteLine("");
    }

    public static void ViewTasks()
    {
        for (int i = 0; i < taskCount; i++)
        {
            Console.WriteLine("Task " + (i + 1) + ": " + tasks[i]);
        }
        Console.WriteLine("");
    }

    public static void CompleteTask()
    {
        Console.WriteLine("Which task did you complete?");
        bool validInput = int.TryParse(Console.ReadLine(), out int choice);
        bool validRange = 0 < choice && choice <= taskCount;
        if (validInput && validRange)
        {
            int index = choice - 1;
            tasks[index] = tasks[index] + " (Completed)";
            Console.WriteLine("Task marked as completed.");
        }
        else
        {
            Console.WriteLine("Invalid input!");
        }
        Console.WriteLine("");
    }

    public static void Main()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("Choose an Action:            ");
            Console.WriteLine("  (1) Add new Task           ");
            Console.WriteLine("  (2) View registered Tasks  ");
            Console.WriteLine("  (3) Mark Task as completed ");
            Console.WriteLine("  (4) Exit Application       ");

            String? choice = Console.ReadLine();
            Console.WriteLine("");

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    ViewTasks();
                    break;
                case "3":
                    CompleteTask();
                    break;
                case "4":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid Input!\n");
                    break;
            }
        }
    }
}
