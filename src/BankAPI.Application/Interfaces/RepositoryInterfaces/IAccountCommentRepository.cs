using BankAPI.Application.DTOs.AccountCommentDto;
using BankAPI.Domain.Entities;

namespace BankAPI.Application.Interfaces.RepositoryInterfaces;

public interface IAccountCommentRepository
{
    Task<AccountComment> CreateAccountCommentAsync(AccountComment accountComment);
    Task<AccountComment?> GetAccountCommentByIdAsync(int commentId);
    Task SaveChangesAsync();
    Task DeleteAccountCommentAsync(AccountComment accountComment);
    Task<List<AccountComment>> GetCommentsByAccountIdAsync(int accountId);
}