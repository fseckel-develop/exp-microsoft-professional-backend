using Microsoft.AspNetCore.Mvc;

// ----------------------
// UsersController
// Controller exposing CRUD operations for `User` backed by `UserService`.
// Follows simple validation patterns used in the exercises (name + email checks).
// ----------------------
[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(UserService userService, ILogger<UsersController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    // POST /users
    // Validate input, create a user and return 201 with location header.
    [HttpPost]
    public ActionResult<User> Create(User user)
    {
        if (string.IsNullOrWhiteSpace(user.Name))
            return BadRequest(new { Error = "Name is required." });

        if (!EmailValidator.IsValidEmail(user.Email))
            return BadRequest(new { Error = "Invalid email format." });

        var created = _userService.Add(user);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // GET /users
    // Return all users.
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAll() => Ok(_userService.GetAll());

    // GET /users/{id}
    // Return a single user or 404.
    [HttpGet("{id:int}")]
    public ActionResult<User> GetById(int id)
    {
        return _userService.TryGet(id, out var user)
            ? Ok(user)
            : NotFound(new { Error = $"User with ID {id} not found." });
    }

    // PUT /users/{id}
    // Validate and update an existing user.
    [HttpPut("{id:int}")]
    public ActionResult<User> Update(int id, User updatedUser)
    {
        if (string.IsNullOrWhiteSpace(updatedUser.Name))
            return BadRequest(new { Error = "Name is required." });

        if (!EmailValidator.IsValidEmail(updatedUser.Email))
            return BadRequest(new { Error = "Invalid email format." });

        if (!_userService.Update(id, updatedUser))
            return NotFound(new { Error = $"User with ID {id} not found." });

        return Ok(updatedUser);
    }

    // DELETE /users/{id}
    // Remove the user with the given id.
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (!_userService.Remove(id))
            return NotFound(new { Error = $"User with ID {id} not found." });

        return NoContent();
    }
}
