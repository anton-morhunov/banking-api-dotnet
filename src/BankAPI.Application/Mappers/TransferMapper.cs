using BankAPI.Application.DTOs.TransferDto;
using BankAPI.Domain.Entities;

namespace BankAPI.Application.Mappers;

public static class TransferMapper
{
    public static TransferResponseDto ToResponseDto(Transfer transfer)
    {
        return new TransferResponseDto
        {
            TransferId = transfer.TransferId,
            UserId = transfer.UserId,
            CreatedAt = transfer.CreatedAt,
            Amount = transfer.Amount,
            Description = transfer.Description,
            SourceAccountId = transfer.SourceAccountId,
            DestinationAccountId = transfer.DestinationAccountId,
        };
    }

    public static Transfer ToTransferEntity(CreateTransferDto createTransferDto)
    {
        return new Transfer
        {
            Amount = createTransferDto.Amount,
            SourceAccountId = createTransferDto.SourceAccountId,
            DestinationAccountId = createTransferDto.DestinationAccountId,
            UserId = createTransferDto.UserId,
            Description = createTransferDto.Description
        };
    }
}