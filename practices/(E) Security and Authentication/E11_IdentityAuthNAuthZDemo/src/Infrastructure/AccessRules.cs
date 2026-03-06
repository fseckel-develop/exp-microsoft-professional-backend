namespace IdentityAuthNAuthZDemo.Infrastructure;

public static class AccessRules
{
    public static class Roles
    {
        public const string Admin = "Admin";
    }

    public static class Policies
    {
        public const string AdminArea = "AdminArea";
        public const string ITOnly = "ITOnly";
    }

    public static class Claims
    {
        public const string Department = "Department";
        public const string IT = "IT";
    }

    public static class DemoUsers
    {
        public const string AdminEmail = "admin@example.com";
        public const string AdminPassword = "Admin@123";
    }
}