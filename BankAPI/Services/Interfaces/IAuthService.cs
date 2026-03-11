using BankAPI.Models;
using BankAPI.DTO.Auth;

namespace BankAPI.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> LogInAsync(string email, string password);
    string GenerateToken(UserModel user);
    Task<UserResponse> CreateUserAsync(CreateUserRequest createUserRequest);
}