using Microsoft.AspNetCore.Mvc;
using SafeVault.Web.Services;
using SafeVault.Web.Storage;

namespace SafeVault.Web.Controllers
{
    [Route("register")]
    public class RegisterController : Controller
    {
        [HttpPost]
        public IActionResult Register(string username, string password, string email, string role)
        {
            string cleanUsername = InputSanitizer.Sanitize(username);
            string cleanEmail = InputSanitizer.Sanitize(email);
            string passwordHash = PasswordHasher.HashPassword(password);

            Database.InsertUser(cleanUsername, cleanEmail, passwordHash, role);

            return Ok("User registered.");
        }
    }
}
