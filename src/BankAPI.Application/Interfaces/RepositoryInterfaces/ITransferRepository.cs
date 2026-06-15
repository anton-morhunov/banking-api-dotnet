using BankAPI.Domain.Entities;

namespace BankAPI.Application.Interfaces.RepositoryInterfaces;

public interface ITransferRepository
{
    Task<Transfer> CreateTransferAsync(Transfer transfer);
    Task<List<Transfer>> GetAllTransfersByAccountIdAsync(int accountId);
    Task<Transfer?> GetTransferByIdAsync(Guid transferId);
    Task<List<Transfer>> GetOutgoingTransfersByAccountIdAsync(int accountId);
    Task<List<Transfer>> GetIncomingTransfersByAccountIdAsync(int accountId);
    Task<List<Transfer>> GetAllTransfersAsync();
    Task<List<Transfer>> GetAllTransfersByUserIdAsync(int userId);
}