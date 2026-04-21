using BankAPI.Domain.Enums;

namespace BankAPI.Application.DTOs.AccountDto;
public class AccountUpdateDto
{
    public AccountStatus Status { get; set; }
    public AccountType AccountType { get; set; }
    public AccountPlan Plan { get; set; }
}