namespace JwtAuthenticationDemo.Services;

public interface IUserStorage
{
    bool ValidateCredentials(string username, string password);
}