using BankAPI.DTO.Auth;

namespace BankAPI.Services.Interfaces;

public interface IUserService
{ 
    Task<IEnumerable<UserResponse?>> GetAllUsersAsync();
    Task<UserResponse?> GetUserByIdAsync(int id);
}