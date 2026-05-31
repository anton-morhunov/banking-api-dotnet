using BankAPI.Application.DTOs.AccountCommentDto;
using FluentValidation;

namespace BankAPI.Application.Validators.CommentValidators;

public class AccountCommentUpdateValidator : AbstractValidator<AccountCommentUpdateDto>
{
    public AccountCommentUpdateValidator()
    {
        RuleFor(comment => comment.Text)
            .NotEmpty()
            .WithMessage("Text should not be empty")
            .MaximumLength(500)
            .WithMessage("Text should not be longer than 500 characters");
    }
}