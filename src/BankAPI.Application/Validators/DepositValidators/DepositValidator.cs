using BankAPI.Application.DTOs.DepositDto;
using FluentValidation;

namespace BankAPI.Application.Validators.DepositValidators;

public class DepositValidator : AbstractValidator<DepositResponseDto>
{
    public DepositValidator()
    {
       RuleFor(x => x.AccountId)
           .NotEmpty()
           .WithMessage("Account Id cannot be empty");
       
       RuleFor(x => x.UserId)
           .GreaterThan(0)
           .WithMessage("User Id must be greater than zero");
       
       RuleFor(x => x.Amount)
           .GreaterThan(0)
           .WithMessage("Amount must be greater than zero");
    }
}