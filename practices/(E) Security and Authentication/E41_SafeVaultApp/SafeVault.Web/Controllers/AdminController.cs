using Microsoft.AspNetCore.Mvc;
using SafeVault.Web.Services;

namespace SafeVault.Web.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly AuthService _authService;

        public AdminController()
        {
            _authService = new AuthService();
        }

        [HttpGet("dashboard")]
        public IActionResult Dashboard(string username)
        {
            if (!_authService.UserHasRole(username, "Admin"))
                return Unauthorized("Access denied. Admins only.");

            return Ok("Welcome to the Admin Dashboard.");
        }
    }
}
