using BankAPI.Application.DTOs.AccountCommentDto;
using FluentValidation;

namespace BankAPI.Application.Validators.CommentValidators;

public class AccountCommentCreateValidator : AbstractValidator<AccountCommentCreateDto>
{
    public AccountCommentCreateValidator()
    {
        RuleFor(comment => comment.Text)
            .NotEmpty()
            .WithMessage("Text cannot be empty")
            .MaximumLength(500)
            .WithMessage("Text cannot be longer than 500 characters");
        
        RuleFor(comment => comment.UserId)
            .GreaterThan(0)
            .WithMessage("UserId should be greater than zero");
        
        RuleFor(comment => comment.AccountId)
            .GreaterThan(0)
            .WithMessage("Account id should be greater than zero");
    }
}