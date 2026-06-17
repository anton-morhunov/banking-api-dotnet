using BankAPI.Domain.Entities;

namespace BankAPI.Application.Interfaces.RepositoryInterfaces.Users;

public interface IUserRepository
{
    Task<UserModel?> GetUserByEmailAsync(string email);
    Task<UserModel?> GetUserByIdAsync(int id);
    Task SaveUserAsync();
    Task<UserModel> CreateUserAsync(UserModel user);
    Task<List<UserModel>> GetAllUsersAsync();
}