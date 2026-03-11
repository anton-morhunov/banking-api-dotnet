using BankAPI.DTO.Auth;
using BankAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers;

[ApiController]
[Route("api/auth")]

public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest loginRequest)
    {
        var token = await _authService.LogInAsync(loginRequest.Email, loginRequest.PasswordHash);

        if (token is null)
        {
            return  Unauthorized();
        }

        return Ok(token);
    }
}