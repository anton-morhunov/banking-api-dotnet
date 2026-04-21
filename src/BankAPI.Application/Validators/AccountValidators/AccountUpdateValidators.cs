using BankAPI.Application.DTOs.AccountDto;
using FluentValidation;

namespace BankAPI.Application.Validators.AccountValidators;

public class AccountUpdateValidators : AbstractValidator<AccountUpdateDto>
{
    public AccountUpdateValidators()
    {
        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Status should be in in-enum");
        
        RuleFor(x => x.AccountType)
            .IsInEnum()
            .WithMessage("Account type should be in-enum");
        
        RuleFor(x => x.Plan)
            .IsInEnum()
            .WithMessage("Plan should be in-enum");
    }
}