using BankAPI.Application.DTOs.TransferDto;

namespace BankAPI.Application.Interfaces.ServiceInterfaces.Transfers;

public interface ITransferService
{
    Task<TransferResponseDto> CreateTransferAsync(CreateTransferDto createTransferDto);
    Task<List<TransferResponseDto>> GetAllTransfersByAccountIdAsync(int accountId);
    Task<TransferResponseDto?> GetTransferByIdAsync(Guid transferId);
    Task<List<TransferResponseDto>> GetOutgoingTransfersByAccountIdAsync(int accountId);
    Task<List<TransferResponseDto>> GetIncomingTransfersByAccountIdAsync(int accountId);
    Task<List<TransferResponseDto>> GetAllTransfersAsync();
    Task<List<TransferResponseDto>> GetAllTransfersByUserIdAsync(int clientId);
}