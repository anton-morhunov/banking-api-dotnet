using BankAPI.Application.DTOs.ClientDto;
using FluentValidation;

namespace BankAPI.Application.Validators.ClientValidators;

public class ClientCreateValidator : AbstractValidator<ClientCreateDTO>
{
    public ClientCreateValidator()
    {
        RuleFor(x =>x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(50)
            .WithMessage("Name must not exceed 50 characters.")
            .MinimumLength(1)
            .WithMessage("Name must contain at least 1 character.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Address is required.")
            .EmailAddress()
            .WithMessage("Address must not exceed 1 characters.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("PhoneNumber is required")
            .Matches(@"^\+\d{10,15}$");
    }
}