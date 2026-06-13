using BankAPI.Domain.Entities;

namespace BankAPI.Application.Interfaces.RepositoryInterfaces;

public interface IDepositRepository
{
    Task<Deposit> MakeDeposit(Deposit deposit);
    Task<List<Deposit>> GetAllDepositsByAccountId(int accountId);
    Task<Deposit?> GetDepositById(Guid depositId);
}