using BankAPI.Application.DTOs.DepositDto;
using BankAPI.Domain.Entities;

namespace BankAPI.Application.Mappers;

public static class DepositMapper
{
    public static DepositResponseDto ToResponseDto(Deposit deposit)
    {
        return new DepositResponseDto
        {
            DepositId = deposit.DepositId,
            Amount = deposit.Amount,
            AccountId = deposit.AccountId,
            UserId = deposit.UserId,
            CreatedAt = deposit.CreatedAt
        };
    }

    public static Deposit ToDepositEntity(DepositCreateDto depositCreateDto)
    {
        return new Deposit
        {
            Amount = depositCreateDto.Amount,
            AccountId = depositCreateDto.AccountId,
            UserId = depositCreateDto.UserId,
            Description = depositCreateDto.Description
        };
    }
}