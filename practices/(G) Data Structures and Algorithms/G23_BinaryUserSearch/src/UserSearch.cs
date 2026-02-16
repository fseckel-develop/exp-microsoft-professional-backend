public static class UserSearch
{
    public static int BinarySearch(List<User> sortedUsers, string target)
    {
        int leftBound = 0;
        int rightBound = sortedUsers.Count - 1;

        while (leftBound <= rightBound)
        {
            int middle = leftBound + (rightBound - leftBound) / 2;
            int comparison = sortedUsers[middle].Username.CompareTo(target);

            if (comparison == 0)
                return middle;

            if (comparison < 0)
                leftBound = middle + 1;
            else
                rightBound = middle - 1;
        }

        return -1;
    }
}