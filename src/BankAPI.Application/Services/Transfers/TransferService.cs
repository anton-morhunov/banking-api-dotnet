using BankAPI.Application.DTOs.TransferDto;
using BankAPI.Application.Interfaces.RepositoryInterfaces.Transfers;
using BankAPI.Application.Interfaces.RepositoryInterfaces.Accounts;
using BankAPI.Application.Interfaces.RepositoryInterfaces.Users;
using BankAPI.Application.Interfaces.ServiceInterfaces.Transfers;
using BankAPI.Domain.Entities;
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
        Transfer transfer = new Transfer
        {
            Amount = createTransferDto.Amount,
            SourceAccountId = createTransferDto.SourceAccountId,
            DestinationAccountId = createTransferDto.DestinationAccountId,
            UserId = createTransferDto.UserId,
            CreatedAt = DateTime.UtcNow,
            TransferId = Guid.NewGuid(),
            Description = createTransferDto.Description,
            
        };
        
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
            
            throw new Exception("Source Account not found");
        }

        if (sourceAccount.Balance < createTransferDto.Amount)
        {
            _logger.LogWarning(
                "The balance on account is not enough. Account balance is {accountBalance}", 
                sourceAccount.Balance
                );
            
            throw new Exception("The amount is not enough on source account");
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
            
            throw new Exception("Destination Account not found");
        }

        if (sourceAccount.Id == destinationAccount.Id)
        {
            _logger.LogWarning(
                "Source and Destination account are identical"
                );
            
            throw new Exception("The source account is the same as the destination account");
        }

        if (createTransferDto.Amount <= 0)
        {
            _logger.LogWarning(
                "The transfer amount cannot be less or equal to zero"
                );
            
            throw new Exception("The transfer amount is zero or below");
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
            
            throw new Exception("User not found");
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

        TransferResponseDto response = new TransferResponseDto
        {
            TransferId = makeTransfer.TransferId,
            Amount = makeTransfer.Amount,
            SourceAccountId = makeTransfer.SourceAccountId,
            DestinationAccountId = makeTransfer.DestinationAccountId,
            UserId = makeTransfer.UserId,
            CreatedAt = DateTime.UtcNow,
            Description = makeTransfer.Description,
        };
        
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
            var dto = new TransferResponseDto
            {
                Amount = transfer.Amount,
                SourceAccountId = transfer.SourceAccountId,
                DestinationAccountId = transfer.DestinationAccountId,
                UserId = transfer.UserId,
                CreatedAt = transfer.CreatedAt,
                TransferId = transfer.TransferId,
                Description = transfer.Description,
            };
            
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
            
            throw new Exception("Transfer not found");
        }

        var result = new TransferResponseDto
        {
            Amount = transfer.Amount,
            CreatedAt = transfer.CreatedAt,
            Description = transfer.Description,
            SourceAccountId = transfer.SourceAccountId,
            DestinationAccountId = transfer.DestinationAccountId,
            TransferId = transfer.TransferId
        };
        
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
            var dto = new TransferResponseDto
            {
                Amount = transfer.Amount,
                CreatedAt = transfer.CreatedAt,
                Description = transfer.Description,
                SourceAccountId = transfer.SourceAccountId,
                DestinationAccountId = transfer.DestinationAccountId,
                TransferId = transfer.TransferId
            };
            
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
            var dto = new TransferResponseDto
            {
                Amount = transfer.Amount,
                CreatedAt = transfer.CreatedAt,
                Description = transfer.Description,
                SourceAccountId = transfer.SourceAccountId,
                DestinationAccountId = transfer.DestinationAccountId,
                TransferId = transfer.TransferId
            };
            
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
        
        return transfers.Select(x => new TransferResponseDto
        {
            Amount = x.Amount,
            CreatedAt = x.CreatedAt,
            Description = x.Description,
            SourceAccountId = x.SourceAccountId,
            DestinationAccountId = x.DestinationAccountId,
            TransferId = x.TransferId
            
        }).ToList();
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
            var dto = new TransferResponseDto
            {
                Amount = transfer.Amount,
                CreatedAt = transfer.CreatedAt,
                Description = transfer.Description,
                SourceAccountId = transfer.SourceAccountId,
                DestinationAccountId = transfer.DestinationAccountId,
                TransferId = transfer.TransferId
            };
            
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