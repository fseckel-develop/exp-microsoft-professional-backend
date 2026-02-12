using Microsoft.AspNetCore.Mvc;
using SafeVault.Web.Services;

namespace SafeVault.Web.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        private readonly AuthService _authService;

        public LoginController()
        {
            _authService = new AuthService();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (_authService.Authenticate(username, password))
                return Ok("Login successful.");

            return Unauthorized("Invalid username or password.");
        }
    }
}
