using BankAPI.Application.DTOs.AccountDto;
using BankAPI.Domain.Entities;

namespace BankAPI.Application.Mappers;

public static class AccountMapper
{
    public static AccountResponseDto ToResponseDto(AccountModel accountModel)
    {
        return new AccountResponseDto
        {
            Balance = accountModel.Balance,
            Status = accountModel.Status,
            AccountType = accountModel.AccountType,
            ClientId = accountModel.ClientId,
            AccountNumber = accountModel.AccountNumber,
            CreatedAt = accountModel.CreatedAt,
            Plan = accountModel.Plan,
            AccountId = accountModel.Id
        };
    }

    public static AccountModel ToAccountModel(AccountCreateDto accountCreateDto)
    {
        return new AccountModel
        {
            ClientId = accountCreateDto.ClientId,
            AccountType = accountCreateDto.AccountType,
        };
    }
}