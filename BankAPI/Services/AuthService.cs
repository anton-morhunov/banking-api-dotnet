using System.IdentityModel.Tokens.Jwt;
using BankAPI.DTO.Auth;
using BankAPI.Models;
using BankAPI.Repositories.Interfaces;
using BankAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;
using BankAPI.Data.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BankAPI.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<AuthService> _logger;
    private readonly IPasswordHasher<UserModel> _passwordHasher;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        IUserRepository userRepository, 
        ILogger<AuthService> logger,
        IPasswordHasher<UserModel> passwordHasher,
        IOptions<JwtSettings> jwtSettings
        )
    {
        _userRepository = userRepository;
        _logger = logger;
        _passwordHasher = passwordHasher;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<LoginResponse> LogInAsync(string email, string password)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        
        _logger.LogInformation(
            "Getting user with email {email}", 
            email
            );

        if (user == null)
        {
            _logger.LogWarning(
                "User with email {email} not found", 
                email
                );
        }
        
        var result = _passwordHasher.VerifyHashedPassword(
            user, 
            user.PasswordHash, 
            password
            );

        if (result == PasswordVerificationResult.Failed)
        {
            _logger.LogWarning(
                "The password is incorrect"
                );
        }
        
        var token = GenerateToken(user);
        
        _logger.LogInformation(
            "The login was successful"
            );

        return new LoginResponse
        {
            Token = token,
        };
    }

    public string GenerateToken(UserModel user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role,  user.Role.ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)
            );
        
        _logger.LogInformation("JWT key: {key}", _jwtSettings.SecretKey);
        
        var credentials = new SigningCredentials(
            key, 
            SecurityAlgorithms.HmacSha256
            );

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes),
            signingCredentials: credentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<UserResponse> CreateUserAsync(CreateUserRequest createUserRequest)
    {
        var existing = _userRepository.GetUserByEmailAsync(createUserRequest.Email);

        if (existing != null)
        {
            _logger.LogWarning(
                "User with email {email} already exists", 
                existing
                );
        }

        var user = new UserModel
        {
            Email = createUserRequest.Email,
            PasswordHash = createUserRequest.Password,
            Role = createUserRequest.UserRole
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, createUserRequest.Password);

        await _userRepository.CreateUserAsync(user);

        return new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            UserRole = user.Role,
        };
    }
}