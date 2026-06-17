using BankAPI.Application.DTOs.AuthDto;
using BankAPI.Application.DTOs.GoogleAuth;
using BankAPI.Application.Interfaces.ServiceInterfaces.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = BankAPI.Application.DTOs.AuthDto.LoginRequest;

namespace BankAPI.Controllers;

[ApiController]
[Route("api/auth")]

public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IGoogleAuthService _googleAuthService;
    
    public AuthController(
        IAuthService authService, 
        IGoogleAuthService googleAuthService
        )
    {
        _authService = authService;
        _googleAuthService = googleAuthService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest loginRequest)
    {
        var token = await _authService.LogInAsync(loginRequest);

        if (token is null)
        {
            return  Unauthorized();
        }

        return Ok(token);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("register")]
    public async Task<ActionResult<UserResponse>> Register(CreateUserRequest createUserRequest)
    {
        var createUser = await _authService.CreateUserAsync(createUserRequest);
        
        return Ok(createUser);
    }

    [HttpPost("google")]
    public async Task<IActionResult> GoogleLogin(GoogleAuthRequestDto dto)
    {
        var response = await _googleAuthService.GoogleLoginAsync(dto.Credential);

        return Ok(response);
    }
    
}