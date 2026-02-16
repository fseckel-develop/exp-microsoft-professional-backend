class Program
{
    static async Task Main()
    {
        // Fetch users
        List<User> users = await UserApiService.FetchUsersFromAPI();
        Console.WriteLine("\nFetched Users:");
        foreach (var user in users)
        {
            Console.WriteLine(user.Username);
        }


        // Sort users
        UserSorter.SortByUsername(users);
        Console.WriteLine("\nSorted Usernames:");
        foreach (var user in users)
        {
            Console.WriteLine(user.Username);
        }


        // Search
        Console.Write("\nEnter a username to search: ");
        string searchUsername = Console.ReadLine()!;
        int index = UserSearch.BinarySearch(users, searchUsername);
        if (index != -1)
        {
            Console.WriteLine($"User Found: {users[index].Username} - {users[index].Name}");
        }
        else
        {
            Console.WriteLine("User not found.");
        }
    }
}