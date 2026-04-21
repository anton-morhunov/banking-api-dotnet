using BankAPI.Application.DTOs.AuthDto;

namespace BankAPI.Application.Interfaces.ServiceInterfaces;

public interface IUserService
{ 
    Task<IEnumerable<UserResponse?>> GetAllUsersAsync();
    Task<UserResponse?> GetUserByIdAsync(int id);
}