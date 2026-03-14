using BankAPI.DTO.Auth;
using Microsoft.AspNetCore.Mvc;
using BankAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BankAPI.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    public  UserController(
        IUserService userService
        )
    {
        _userService = userService;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUserAsync()
    {
        var users = await _userService.GetAllUsersAsync();

        if (!users.Any())
        {
            return NoContent();
        }
        return Ok(users);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("getUserById")]
    public async Task<ActionResult<UserResponse?>> GetUserByIdAsync(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user is null)
        {
            return NoContent();
        }
        
        return  Ok(user);
    }
}