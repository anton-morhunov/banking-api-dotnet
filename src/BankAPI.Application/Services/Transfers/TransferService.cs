using BankAPI.Application.DTOs.TransferDto;
using BankAPI.Application.Exceptions;
using BankAPI.Application.Interfaces.RepositoryInterfaces.Transfers;
using BankAPI.Application.Interfaces.RepositoryInterfaces.Accounts;
using BankAPI.Application.Interfaces.RepositoryInterfaces.Users;
using BankAPI.Application.Interfaces.ServiceInterfaces.Transfers;
using BankAPI.Application.Mappers;
using Microsoft.Extensions.Logging;

namespace BankAPI.Application.Services.Transfers;

public class TransferService : ITransferService
{
    private readonly ITransferRepository _transferRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<TransferService> _logger;
    
    public TransferService(
        ITransferRepository transferRepository,  
        IAccountRepository accountRepository,
        IUserRepository userRepository,
        ILogger<TransferService> logger
        )
    {
        _transferRepository = transferRepository;
        _accountRepository = accountRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<TransferResponseDto> CreateTransferAsync(CreateTransferDto createTransferDto)
    {
        var transfer = TransferMapper.ToTransferEntity(createTransferDto);
        
        transfer.CreatedAt = DateTime.UtcNow;
        transfer.TransferId = Guid.NewGuid();
        
        var sourceAccount = await _accountRepository.GetAccountAsync(createTransferDto.SourceAccountId);
        
        _logger.LogInformation(
            "Loading source account {accountId}", 
            createTransferDto.SourceAccountId
            );

        if (sourceAccount is null)
        {
            _logger.LogWarning(
                "Source account with ID: {accountId} does not exist", 
                createTransferDto.SourceAccountId
                );
            
            throw new NotFoundException($"Source account with {transfer.SourceAccountId} does not exist");
        }

        if (sourceAccount.Balance < createTransferDto.Amount)
        {
            _logger.LogWarning(
                "The balance on account is not enough. Account balance is {accountBalance}", 
                sourceAccount.Balance
                );
            
            throw new BadRequestException(
                $"The source  account balance is {sourceAccount.Balance} while transfer amount is {createTransferDto.Amount}"
                );
        }

        var destinationAccount = await _accountRepository.GetAccountAsync(createTransferDto.DestinationAccountId);
        
        _logger.LogInformation(
            "Loading destination account {accountId}", 
            createTransferDto.DestinationAccountId
            );

        if (destinationAccount is null)
        {
            _logger.LogWarning(
                "Destination account with ID: {accountId} does not exist", 
                createTransferDto.DestinationAccountId
                );
            
            throw new NotFoundException($"Destination account with {transfer.SourceAccountId} does not exist");
        }

        if (sourceAccount.Id == destinationAccount.Id)
        {
            _logger.LogWarning(
                "Source and Destination account are identical"
                );
            
            throw new BadRequestException("Source and Destination account are identical");
        }

        if (createTransferDto.Amount <= 0)
        {
            _logger.LogWarning(
                "The transfer amount cannot be less or equal to zero"
                );
            
            throw new BadRequestException("The transfer amount cannot be less or equal to zero");
        }
        
        var user = await _userRepository.GetUserByIdAsync(createTransferDto.UserId);
        
        _logger.LogInformation(
            "Loading user with ID {userId}", 
            createTransferDto.UserId
            );

        if (user is null)
        {
            _logger.LogWarning(
                "User with ID: {userId} does not exist", 
                createTransferDto.UserId
                );
            
            throw new  NotFoundException($"User with ID: {createTransferDto.UserId} does not exist");
        }
        
        sourceAccount.Balance -= createTransferDto.Amount;
        
        _logger.LogInformation(
            "Getting amount {amount} for transfer from account {sourceAccountId}", 
            createTransferDto.Amount, 
            createTransferDto.SourceAccountId
            );
        
        destinationAccount.Balance += createTransferDto.Amount;
        
        _logger.LogInformation(
            "Adding amount {amount} for transfer to account {destinationAccountId}", 
            createTransferDto.Amount,
            createTransferDto.DestinationAccountId 
            );

        var makeTransfer = await _transferRepository.CreateTransferAsync(transfer);

        await _accountRepository.SaveAsync();

        var response = TransferMapper.ToResponseDto(makeTransfer);
        
        _logger.LogInformation(
            "Transfer with ID:{transferId} successfully created", 
            makeTransfer.TransferId
            );
        
        return response;
    }

    public async Task<List<TransferResponseDto>> GetAllTransfersByAccountIdAsync(int  accountId)
    {
        var transfers = await _transferRepository.GetAllTransfersByAccountIdAsync(accountId);
        
        var response = new List<TransferResponseDto>();

        foreach (var transfer in transfers)
        {
            var dto = TransferMapper.ToResponseDto(transfer);
            
            response.Add(dto);
        }
        
        _logger.LogInformation(
            "Got {transferCount} transfers for account {accountId}", 
            transfers.Count, 
            accountId
            );
        
        return response;
    }

    public async Task<TransferResponseDto?> GetTransferByIdAsync(Guid transferId)
    {
        var transfer = await _transferRepository.GetTransferByIdAsync(transferId);

        if (transfer is null)
        {
            _logger.LogWarning(
                "Transfer with ID: {transferId} does not exist", 
                transferId
                );
            
            throw new  NotFoundException($"Transfer with ID: {transferId} does not exist");
        }

        var result = TransferMapper.ToResponseDto(transfer);
        
        _logger.LogInformation(
            "Transfer with ID:{transferId} was found", 
            transfer.TransferId
            );
        
        return result;
    }

    public async Task<List<TransferResponseDto>> GetOutgoingTransfersByAccountIdAsync(int accountId)
    {
        _logger.LogInformation(
            "Getting outgoing transfer for account {accountId}", 
            accountId
            );
        
        var outgoingTransfers = await _transferRepository.GetOutgoingTransfersByAccountIdAsync(accountId);

        var response = new List<TransferResponseDto>();

        foreach (var transfer in outgoingTransfers)
        {
            var dto = TransferMapper.ToResponseDto(transfer);
            
            response.Add(dto);
        }
        
        _logger.LogInformation(
            "Got {transfersCount} outgoing transfers for account {accountId}", 
            outgoingTransfers.Count, 
            accountId
            );
        
        return response;
    }

    public async Task<List<TransferResponseDto>> GetIncomingTransfersByAccountIdAsync(int accountId)
    {
        _logger.LogInformation(
            "Getting incoming transfer for account {accountId}", 
            accountId
            );
        
        var incomingTransfers = await _transferRepository.GetIncomingTransfersByAccountIdAsync(accountId);

        var response = new List<TransferResponseDto>();

        foreach (var transfer in incomingTransfers)
        {
            var dto = TransferMapper.ToResponseDto(transfer);
            
            response.Add(dto);
        }
        
        _logger.LogInformation(
            "Got {transfersCount} incoming transfers for account {accountId}", 
            incomingTransfers.Count, 
            accountId
            );
        
        return response;
    }

    public async Task<List<TransferResponseDto>> GetAllTransfersAsync()
    {
        _logger.LogInformation(
            "Getting all transfers"
            );
        
        var transfers = await _transferRepository.GetAllTransfersAsync();
        
        _logger.LogInformation(
            "Got {transfersCount} transfers", 
            transfers.Count
            );
        
        return transfers.Select(TransferMapper.ToResponseDto).ToList();
    }

    public async Task<List<TransferResponseDto>> GetAllTransfersByUserIdAsync(int userId)
    {
        _logger.LogInformation(
            "Getting  all transfers for user {userId}", 
            userId
            );
        
        var transfers = await _transferRepository.GetAllTransfersByUserIdAsync(userId);

        var response = new List<TransferResponseDto>();

        foreach (var transfer in transfers)
        {
            var dto = TransferMapper.ToResponseDto(transfer);
            
            response.Add(dto);
        }
        
        _logger.LogInformation(
            "Got {transfersCount} transfers for user {userId}", 
            transfers.Count, 
            userId
            );
        
        return response;
    }
}