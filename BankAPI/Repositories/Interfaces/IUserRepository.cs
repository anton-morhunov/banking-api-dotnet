using BankAPI.Models;

namespace BankAPI.Repositories.Interfaces;

public interface IUserRepository
{
    Task<UserModel?> GetUserByEmailAsync(string email);
    Task<UserModel?> GetUserByIdAsync(int id);
    Task SaveUserAsync();
    Task<UserModel> CreateUserAsync(UserModel user);
}