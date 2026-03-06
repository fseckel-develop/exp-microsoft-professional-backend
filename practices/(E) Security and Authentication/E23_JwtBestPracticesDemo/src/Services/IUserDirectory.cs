namespace JwtBestPracticesDemo.Services;

public interface IUserDirectory
{
    bool TryValidate(string username, string password, out string role);
}