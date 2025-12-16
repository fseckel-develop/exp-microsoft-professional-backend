using System.Net.Mail;

// ----------------------
// EmailValidator
// Small helper for validating email addresses used across controllers/services.
// Uses System.Net.Mail.MailAddress for basic format checks.
// ----------------------
public static class EmailValidator
{
    // Returns true when the provided email parses as a valid address
    public static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
