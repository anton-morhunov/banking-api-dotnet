using BankAPI.Application.DTOs.AccountDto;
using BankAPI.Application.Exceptions;
using BankAPI.Application.Interfaces.RepositoryInterfaces.Accounts;
using BankAPI.Application.Interfaces.ServiceInterfaces.Accounts;
using BankAPI.Application.Mappers;
using BankAPI.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace BankAPI.Application.Services.Accounts;
public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ILogger<AccountService> _logger;
    public AccountService(IAccountRepository accountRepository, 
        ILogger<AccountService> logger)
    {
        _accountRepository = accountRepository;
        _logger = logger;
    }

    public async Task<AccountResponseDto> GetAccountByIdAsync(
        int? accountId
        )
    {
        _logger.LogInformation(
            "Getting account {AccountId}", 
            accountId
            );
        
        var account = await _accountRepository.GetAccountAsync(accountId
            );

        if (account == null)
        {
            _logger.LogWarning(
                "Account{AccountId} not found",
                accountId
                );
            
            throw new NotFoundException($"Account {accountId} not found");
        }
        
        var response = AccountMapper.ToResponseDto(account);
        
        _logger.LogInformation(
            "Account {AccountId} retrieved", 
            accountId
            );
        
        return response;
    }

    public async Task<AccountResponseDto> CreateAccount(AccountCreateDto  accountCreateDto)
    {
        _logger.LogInformation(
            "Creating account"
            );

        var account = AccountMapper.ToAccountModel(accountCreateDto);

        account.AccountNumber = Guid.NewGuid().ToString();
        account.Balance = 0;
        account.CreatedAt = DateTime.UtcNow;
        account.Status = AccountStatus.Active;

        var createdAccount = await _accountRepository.CreateAccountAsync(account);
        
        var response = AccountMapper.ToResponseDto(createdAccount);

        await _accountRepository.SaveAsync();
        
        _logger.LogInformation(
            "Account {AccountNumber} created", 
            createdAccount.AccountNumber
            );
        
        return response;
    }

    public async Task<List<AccountResponseDto>> GetAllAccountsByClientIdAsync(int clientId)
    {
        var accounts = await _accountRepository.GetAllAccountsByClientIdAsync(clientId);

        var response = new List<AccountResponseDto>();
        
        foreach (var account in accounts)
        {
            var dto = AccountMapper.ToResponseDto(account);
            
            response.Add(dto);
        }
        
        _logger.LogInformation(
            "Got {accountCount} accounts from the database",
            response.Count
            );
        
        return response;
    }

    public async Task<AccountResponseDto> AccountUpdateStatusAsync(
        int accountId, 
        AccountUpdateDto accountUpdateDto
        )
    {
        _logger.LogInformation(
            "Updating account {AccountId} status {Status}", 
            accountId, 
            accountUpdateDto.Status
            );
        
        var account = await _accountRepository.GetAccountAsync(accountId);

        if (account == null)
        {
            _logger.LogWarning(
                "Account{AccountId} not found", 
                accountId
                );
            
            throw new NotFoundException($"Account {accountId} not found");
        }

        var oldStatus = account.Status;
        account.Status = accountUpdateDto.Status;

        await _accountRepository.SaveAsync();

        var response = AccountMapper.ToResponseDto(account);
        
        _logger.LogInformation(
            "Account {AccountId} status was updated from {OldStatus} to {NewStatus}", 
            accountId, 
            oldStatus, 
            response.Status
            );
        
        return response;
    }

    public async Task<bool> CloseAccountAsync(
        int accountId
        )
    {
        _logger.LogInformation(
            "Closing Account {AccountId}", 
            accountId
            );
        
        var account = await _accountRepository.GetAccountAsync(
            accountId
            );

        if (account == null)
        {
            _logger.LogWarning(
                "Account {AccountId} was not found", 
                accountId
                );
            
            return false;
        }
        
        account.Status = AccountStatus.Closed;
        await _accountRepository.SaveAsync();
        
        _logger.LogInformation(
            "Account {AccountId} closed", 
            accountId
            );
        
        return true;
    }

    public async Task<AccountResponseDto> AccountUpdatePlanAsync(
        int accountId, 
        AccountUpdateDto accountUpdateDto
        )
    {
        _logger.LogInformation(
            "Updating plan of account {AccountId}", 
            accountId
            );
        
        var account = await _accountRepository.GetAccountAsync(accountId);

        if (account == null)
        {
            _logger.LogWarning(
                "Account {AccountId} was not found", 
                accountId
                );
            
            throw new NotFoundException($"Account {accountId} not found");
        }

        if (account.Status == AccountStatus.Closed)
        {
            _logger.LogWarning(
                "Account {AccountId} status is closed", 
                accountId
                );
            
            throw new BadRequestException("Account status is closed");
        }
        
        var oldPlan = account.Plan;
        account.Plan =  accountUpdateDto.Plan;
        await _accountRepository.SaveAsync();

        var response = AccountMapper.ToResponseDto(account);
        
        _logger.LogInformation(
            "Changed account{AccountId} plan from {OldPlan} to {NewPlan}", 
            accountId, 
            oldPlan, 
            response.Plan
            );
        
        return response;
    }

    public async Task<IEnumerable<AccountResponseDto>> GetAllAccounts()
    {
        _logger.LogDebug(
            "Getting all accounts"
            );

        var accounts = await _accountRepository.GetAllAccountsAsync();

        return accounts.Select(AccountMapper.ToResponseDto);
    }
}