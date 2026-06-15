using BankAPI.Application.DTOs.TransferDto;
using FluentValidation;

namespace BankAPI.Application.Validators.TransferValidators;

public class TransferValidator : AbstractValidator<CreateTransferDto>
{
    public  TransferValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User Id must be not empty")
            .GreaterThan(0)
            .WithMessage("User Id must be greater than zero");
        
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than zero");
        
        RuleFor(x => x.SourceAccountId)
            .NotEmpty()
            .WithMessage("Source ID must be not empty")
            .GreaterThan(0)
            .WithMessage("SourceAccountId must be greater than zero");
        
        RuleFor(x => x.DestinationAccountId)
            .NotEmpty()
            .WithMessage("Destination ID must be not empty")
            .GreaterThan(0)
            .WithMessage("DestinationAccountId must be greater than zero");
        
        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("Description must not exceed 500 characters");
        
        RuleFor(x => x.SourceAccountId)
            .NotEqual(x => x.DestinationAccountId)
            .WithMessage("Source and destination accounts must be different");
    }
}