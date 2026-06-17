using BankAPI.Application.DTOs.AuthDto;

namespace BankAPI.Application.Interfaces.ServiceInterfaces.Authentication;
    
public interface IAuthService
{
    Task<LoginResponse> LogInAsync(LoginRequest loginRequest);
    Task<UserResponse> CreateUserAsync(CreateUserRequest createUserRequest);
}