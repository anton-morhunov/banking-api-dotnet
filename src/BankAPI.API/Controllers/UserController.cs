using BankAPI.Application.DTOs.AuthDto;
using Microsoft.AspNetCore.Mvc;
using BankAPI.Application.Interfaces.ServiceInterfaces;
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
            return NotFound();
        }
        return Ok(users);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserResponse?>> GetUserByIdAsync(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user is null)
        {
            return NotFound();
        }
        
        return  Ok(user);
    }
}