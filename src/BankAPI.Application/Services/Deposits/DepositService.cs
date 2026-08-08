using BankAPI.Application.DTOs.DepositDto;
using BankAPI.Application.Exceptions;
using BankAPI.Application.Interfaces.RepositoryInterfaces.Deposits;
using BankAPI.Application.Interfaces.ServiceInterfaces.Deposits;
using BankAPI.Application.Interfaces.RepositoryInterfaces.Accounts;
using BankAPI.Application.Interfaces.RepositoryInterfaces.Users;
using BankAPI.Application.Mappers;
using BankAPI.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BankAPI.Application.Services.Deposits;

public class DepositService : IDepositService
{
    private readonly IDepositRepository _depositRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<DepositService> _logger;
    
    public DepositService(
        IDepositRepository depositRepository,  
        IAccountRepository accountRepository,
        IUserRepository userRepository,
        ILogger<DepositService> logger
        )
    {
        _depositRepository = depositRepository;
        _accountRepository = accountRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<DepositResponseDto> MakeDeposit(DepositCreateDto depositCreateDto)
    {
        _logger.LogInformation(
            "Creating deposit. UserId = {userId} , " +
            "AccountId = {accountId} , " +
            "Amount = {amount} , " ,
            depositCreateDto.UserId,  
            depositCreateDto.AccountId,
            depositCreateDto.Amount
            );
        
        var deposit = DepositMapper.ToDepositEntity(depositCreateDto);
        
        deposit.CreatedAt = DateTime.UtcNow;
        deposit.DepositId = Guid.NewGuid();
        
        if (depositCreateDto.Amount <= 0)
        {
            _logger.LogWarning(
                "Amount should be greater than zero"
            );
            
            throw new BadRequestException("Amount should be greater than zero");
        }
        
        var account = await _accountRepository.GetAccountAsync(depositCreateDto.AccountId);
        
        if (account is null)
        {
            _logger.LogWarning(
                "Account {accountId} does not exist", 
                depositCreateDto.AccountId
                );
            
            throw new NotFoundException($"Account with Id {depositCreateDto.AccountId} not found");
        }

        var user = await _userRepository.GetUserByIdAsync(depositCreateDto.UserId);

        if (user is null)
        {
            _logger.LogWarning(
                "User {userId} does not exist", 
                depositCreateDto.UserId
                );
            
            throw new NotFoundException($"User with Id {depositCreateDto.UserId} not found");
        }
        
        account.Balance += depositCreateDto.Amount;
        
        var makeDeposit = await _depositRepository.MakeDeposit(deposit);

        await _accountRepository.SaveAsync();

        var response = DepositMapper.ToResponseDto(makeDeposit);
        
        _logger.LogInformation(
            "Deposit was created successfully"
            );
        
        return  response;

    }

    public async Task<List<DepositResponseDto>> GetAllDepositsByAccountId(int accountId)
    {
        var deposits = await _depositRepository.GetAllDepositsByAccountId(accountId);

        var response = new List<DepositResponseDto>();

        foreach (var deposit in deposits)
        {
            var dto = DepositMapper.ToResponseDto(deposit);
            
            response.Add(dto);
        }
        
        _logger.LogInformation(
            "Got {depositCount} deposits for accountId {accountId}", 
            deposits.Count, 
            accountId
            );
        
        return response;
    }

    public async Task<DepositResponseDto> GetDepositById(Guid depositId)
    {
        var deposit = await _depositRepository.GetDepositById(depositId);

        if (deposit is null)
        {
            _logger.LogWarning(
                "Deposit {depositId} does not exist", 
                depositId
                );
            
            throw new NotFoundException($"Deposit with Id {depositId} not found");
        }

        var result = DepositMapper.ToResponseDto(deposit);
        
        _logger.LogInformation(
            "Deposit {depositId} was found",
            depositId
            );
        
        return result;
    }
}