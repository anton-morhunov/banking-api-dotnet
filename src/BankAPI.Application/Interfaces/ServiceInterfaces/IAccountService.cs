using BankAPI.Application.DTOs.AccountDto;

namespace BankAPI.Application.Interfaces.ServiceInterfaces;

public interface IAccountService
{
    Task<AccountResponseDto> CreateAccount(AccountCreateDto accountCreateDto);
    Task<AccountResponseDto?> GetAccountByIdAsync(int? accountId);
    Task<List<AccountResponseDto>> GetAllAccountsByClientIdAsync(int clientId);
    Task<AccountResponseDto?> AccountUpdateStatusAsync(int accountId, 
        AccountUpdateDto accountUpdateDto);
    Task<AccountResponseDto?> AccountUpdatePlanAsync(
        int accountId, 
        AccountUpdateDto accountUpdateDto);
    Task<bool> CloseAccountAsync(int accountId);
    Task<IEnumerable<AccountResponseDto>> GetAllAccounts();
}