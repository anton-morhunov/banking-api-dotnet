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

    public async Task<LoginResponse> LogInAsync(string email, string password)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);

        _logger.LogInformation("Getting user with email {email}", email);

        if (user == null)
        {
            _logger.LogWarning("User with email {email} not found", email);
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        var isValid = _passwordService.Verify(password, user.PasswordHash);

        if (!isValid)
        {
            _logger.LogWarning("Invalid password for {email}", email);
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        var token = _jwtService.GenerateToken(user);

        _logger.LogInformation("Login successful for {email}", email);

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