using System.Text;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

var key = Convert.FromBase64String("3vZy0l6w1zvF+g4Q0k2N0Qw5FzN+0b9M8pJ2W9RkPqU=");
var tokenHandler = new JwtSecurityTokenHandler();

var tokenDescriptor = new SecurityTokenDescriptor
{
    Subject = new ClaimsIdentity(new[]
    {
        new Claim(ClaimTypes.NameIdentifier, "testuser@example.com") // important!
    }),
    Expires = DateTime.UtcNow.AddHours(1),
    Issuer = "SecureApi",
    Audience = "SecureApiUsers",
    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
};

var token = tokenHandler.CreateToken(tokenDescriptor);
var jwt = tokenHandler.WriteToken(token);
Console.WriteLine(jwt);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("SecureDataDb")
);

builder.Services.AddSingleton<IEncryptionService, EncryptionService>();

var jwtKey = Convert.FromBase64String(
    builder.Configuration["Authentication:Schemes:Bearer:SigningKeys:0:Value"]
    ?? throw new NullReferenceException("JWT Signing Key not found in configuration.")
);

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Authentication:Schemes:Bearer:ValidIssuer"],
        ValidAudience = builder.Configuration["Authentication:Schemes:Bearer:ValidAudience"]
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/messages", async (AppDbContext dbContext) =>
{
    var messages = await dbContext.Messages.ToListAsync();
    return Results.Ok(messages);
});

app.MapGet("/messages/{id}", async (IEncryptionService encryptionService, AppDbContext dbContext, int id) =>
{
    var message = await dbContext.Messages.FindAsync(id);
    if (message == null)
    {
        return Results.NotFound("Message does not exist.");
    }
    return Results.Ok(encryptionService.Decrypt(message.Text));
});

app.MapPost("/messages", async (IEncryptionService encryptionService, HttpContext httpContext, AppDbContext dbContext, MessageDto messageDto) =>
{
    // In a real application, you would extract the user from the authenticated context (e.g., JWT token)
    var user = "testuser@example.com";

    if (string.IsNullOrEmpty(user))
    {
        return Results.BadRequest("Email required to post message.");
    }

    var message = new Message
    {
        Text = encryptionService.Encrypt(messageDto.Text),
        User = user
    };

    dbContext.Messages.Add(message);
    await dbContext.SaveChangesAsync();

    return Results.Created($"/messages/{message.Id}", messageDto.Text);
});

app.Run();


public record MessageRequest(string message);

public interface IEncryptionService
{
    string Encrypt(string plainText);
    string Decrypt(string cypherText);
}

internal class EncryptionService : IEncryptionService
{
    private readonly byte[] _key;
    private readonly byte[] _iv;  // initialization vector (secondary key for encryption algorithms)

    public EncryptionService(IConfiguration configuration)
    {
        _key = Encoding.UTF8.GetBytes(configuration["Encryption:Key"]!)
            ?? throw new NullReferenceException("Encryption key not found in configuration.");
        _iv = Encoding.UTF8.GetBytes(configuration["Encryption:IV"]!)
            ?? throw new NullReferenceException("Encryption IV not found in configuration.");
    }

    public string Encrypt(string plainText)
    {
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using var memoryStream = new MemoryStream();
        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
        using (var streamWriter = new StreamWriter(cryptoStream))
        {
            streamWriter.Write(plainText);
            streamWriter.Flush();
        }
        return Convert.ToBase64String(memoryStream.ToArray());
    }

    public string Decrypt(string cypherText)
    {
        if (string.IsNullOrEmpty(cypherText))
            throw new ArgumentNullException(nameof(cypherText));
        var buffer = Convert.FromBase64String(cypherText);
        
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using var memoryStream = new MemoryStream(buffer);
        using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        using var streamReader = new StreamReader(cryptoStream);

        return streamReader.ReadToEnd();
    }
}

public class MessageDto
{
    public required string Text { get; set; }
}

public class Message
{
    public int Id { get; set; }
    public required string Text { get; set; }
    public required string User { get; set; }
}

public class AppDbContext : DbContext
{
    public DbSet<Message> Messages { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
}
