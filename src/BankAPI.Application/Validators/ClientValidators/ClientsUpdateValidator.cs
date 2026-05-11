using BankAPI.Application.DTOs.ClientDto;
using FluentValidation;

namespace BankAPI.Application.Validators.ClientValidators;

public class ClientsUpdateValidator : AbstractValidator<ClientUpdateDTO>
{
    public ClientsUpdateValidator()
    {
        RuleFor(client => client.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MinimumLength(1)
            .WithMessage("Name must contain at least 1 character.")
            .MaximumLength(50)
            .WithMessage("Name must not exceed 50 characters.");
        
        RuleFor(client => client.Email)
            .NotEmpty()
            .WithMessage("Mail is required")
            .EmailAddress()
            .WithMessage("Invalid email address");
        
        RuleFor(client => client.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required")
            .Matches(@"^\+\d{10,15}$");;
    }
}