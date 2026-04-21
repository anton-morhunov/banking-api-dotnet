using BankAPI.Domain.Entities;

namespace BankAPI.Application.Interfaces.RepositoryInterfaces;

public interface IAccountRepository
{
    Task<AccountModel?> GetAccountAsync(int accountId, int clientId);
    Task<List<AccountModel>> GetAllAccountsByClientIdAsync(int clientId);
    Task<AccountModel> CreateAccountAsync(AccountModel account);
    Task SaveAsync();
}