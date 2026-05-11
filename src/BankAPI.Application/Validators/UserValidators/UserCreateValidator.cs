using BankAPI.Application.DTOs.AuthDto;
using FluentValidation;

namespace BankAPI.Application.Validators.UserValidators;

public class UserCreateValidator : AbstractValidator<CreateUserRequest>
{
    public UserCreateValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email is invalid");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required");
    }
}