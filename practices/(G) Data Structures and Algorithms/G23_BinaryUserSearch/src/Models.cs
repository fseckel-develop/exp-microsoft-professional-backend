public class User
{
    public required string Username { get; set; }
    public required string Name { get; set; }
}

public class ApiResponse
{
    public required List<ApiResult> Results { get; set; }
}

public class ApiResult
{
    public required Name Name { get; set; }
    public required Login Login { get; set; }
}

public class Name
{
    public required string First { get; set; }
    public required string Last { get; set; }
}

public class Login
{
    public required string Username { get; set; }
}