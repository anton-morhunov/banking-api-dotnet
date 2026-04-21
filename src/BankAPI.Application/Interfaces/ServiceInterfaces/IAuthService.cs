using BankAPI.Application.DTOs.AuthDto;

namespace BankAPI.Application.Interfaces.ServiceInterfaces;
    
public interface IAuthService
{
    Task<LoginResponse> LogInAsync(string email, string password);
    Task<UserResponse> CreateUserAsync(CreateUserRequest createUserRequest);
}