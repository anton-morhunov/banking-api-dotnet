using BankAPI.Domain.Enums;

namespace BankAPI.Application.DTOs.AccountDto;

public class AccountCreateDto
{
    public int ClientId { get; set; }
    public AccountType AccountType { get; set; }
}