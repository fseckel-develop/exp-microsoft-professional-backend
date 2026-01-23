using System;
using System.Text.Json;
using System.Security.Cryptography;
using System.Text;

public class User
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }

    public void EncryptData()
    {
        // Encrypt Sensitive Data Before Serialization:
        Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(Password));
    }

    public string GenerateHash()
    {
        // Hash for Data Integrity Check:
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(ToString()));
            return Convert.ToBase64String(hashBytes);
        }
    }

    public override string ToString() => JsonSerializer.Serialize(this);
}

public class Program
{
    public static void Main()
    {
        User user = new User { Name = "Alice", Email = "alice@example.com", Password = "SecureP@ss123" };

        string generatedHash = user.GenerateHash();
        Console.WriteLine("Generated Hash: " + generatedHash);
        string serializedData = SerializeUserData(user);
        Console.WriteLine("Serialized Data:\n" + serializedData);

        string trustedSourceData = serializedData; // Assume this is from a trusted source
        User deserializedUser = DeserializeUserData(trustedSourceData, isTrustedSource: true);

        if (deserializedUser != null)
        {
            Console.WriteLine("Deserialization successful for trusted source.");
        }
    }

    public static string SerializeUserData(User user)
    {
        // Data Input Validation:
        if (user == null 
            || string.IsNullOrWhiteSpace(user.Name) 
            || string.IsNullOrWhiteSpace(user.Email) 
            || string.IsNullOrWhiteSpace(user.Password))
        {
            Console.WriteLine("Invalid data. Serialization aborted.");
            return string.Empty;
        }
        user.EncryptData();
        return JsonSerializer.Serialize(user);
    }

    public static User DeserializeUserData(string jsonData, bool isTrustedSource)
    {
        // Prevent Deserialization of Untrusted Data:
        if (!isTrustedSource)
        {
            Console.WriteLine("Deserialization blocked: Untrusted source.");
            return null;
        }
        return JsonSerializer.Deserialize<User>(jsonData);
    }
}
