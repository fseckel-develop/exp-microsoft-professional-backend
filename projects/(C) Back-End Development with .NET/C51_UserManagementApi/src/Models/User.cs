// ----------------------
// User model
// Simple POCO used by controllers and the in-memory `UserService`.
// Kept minimal for teaching examples: `Id`, `Name`, `Email`.
// ----------------------
public class User
{
    // Numeric identifier assigned by `UserService`.
    public int Id { get; set; }

    // User's display name (required in examples)
    public required string Name { get; set; }

    // User's email address (validated by EmailValidator)
    public required string Email { get; set; }
}
