public static class UserSorter
{
    public static void SortByUsername(List<User> users)
    {
        users.Sort((a, b) => a.Username.CompareTo(b.Username));
    }
}