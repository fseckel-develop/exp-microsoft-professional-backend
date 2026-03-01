using Microsoft.AspNetCore.Mvc;
using UserManagementApi.Contracts;
using UserManagementApi.Models;
using UserManagementApi.Services;
using UserManagementApi.Validation;

namespace UserManagementApi.Controllers;

[ApiController]
[Route("users")]
public sealed class UsersController : ControllerBase
{
    private readonly UserService _userService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(UserService userService, ILogger<UsersController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpPost]
    public ActionResult<User> Create([FromBody] CreateUserRequestDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return BadRequest(new { error = "Name is required." });

        if (!EmailValidator.IsValidEmail(dto.Email))
            return BadRequest(new { error = "Invalid email format." });

        var user = new User
        {
            Name = dto.Name.Trim(),
            Email = dto.Email.Trim()
        };

        var created = _userService.Add(user);

        _logger.LogInformation("Created user with ID {UserId}", created.Id);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAll()
    {
        return Ok(_userService.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<User> GetById(int id)
    {
        return _userService.TryGet(id, out var user)
            ? Ok(user)
            : NotFound(new { error = $"User with ID {id} not found." });
    }

    [HttpPut("{id:int}")]
    public ActionResult<User> Update(int id, [FromBody] UpdateUserRequestDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return BadRequest(new { error = "Name is required." });

        if (!EmailValidator.IsValidEmail(dto.Email))
            return BadRequest(new { error = "Invalid email format." });

        var updatedUser = new User
        {
            Name = dto.Name.Trim(),
            Email = dto.Email.Trim()
        };

        if (!_userService.Update(id, updatedUser))
            return NotFound(new { error = $"User with ID {id} not found." });

        return Ok(updatedUser);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (!_userService.Remove(id))
            return NotFound(new { error = $"User with ID {id} not found." });

        return NoContent();
    }
}