using BankAPI.Application.DTOs.AuthDto;
using FluentValidation;

namespace BankAPI.Application.Validators.Login_Validators;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}