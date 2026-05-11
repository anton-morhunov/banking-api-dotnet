using BankAPI.Application.DTOs.AuthDto;
using BankAPI.Domain.Entities;
using BankAPI.Application.Interfaces.RepositoryInterfaces;
using BankAPI.Application.Interfaces.ServiceInterfaces;
using Microsoft.Extensions.Logging;

namespace BankAPI.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<AuthService> _logger;
    private readonly IPasswordService _passwordService;
    private readonly IJwtService _jwtService;

    public AuthService(
        IUserRepository userRepository,
        ILogger<AuthService> logger,
        IPasswordService passwordService,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _logger = logger;
        _passwordService = passwordService;
        _jwtService = jwtService;
    }

    public async Task<LoginResponse> LogInAsync(LoginRequest loginRequest)
    {
        var user = await _userRepository.GetUserByEmailAsync(loginRequest.Email);

        _logger.LogInformation("Getting user with email {email}", loginRequest.Email);

        if (user == null)
        {
            _logger.LogWarning("User with email {email} not found", loginRequest.Email);
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        var isValid = _passwordService.Verify(loginRequest.PasswordHash, user.PasswordHash);

        if (!isValid)
        {
            _logger.LogWarning("Invalid password for {email}", loginRequest.Email);
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        var token = _jwtService.GenerateToken(user);

        _logger.LogInformation("Login successful for {email}", loginRequest.Email);

        return new LoginResponse
        {
            Token = token
        };
    }

    public async Task<UserResponse> CreateUserAsync(CreateUserRequest request)
    {
        var existing = await _userRepository.GetUserByEmailAsync(request.Email);

        if (existing != null)
        {
            _logger.LogWarning("User with email {email} already exists", request.Email);
            throw new InvalidOperationException("User already exists");
        }

        var user = new UserModel
        {
            Email = request.Email,
            PasswordHash = _passwordService.Hash(request.Password),
            Role = request.UserRole
        };

        await _userRepository.CreateUserAsync(user);

        return new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            UserRole = user.Role
        };
    }
}