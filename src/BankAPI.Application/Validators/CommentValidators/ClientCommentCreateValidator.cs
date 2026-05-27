using BankAPI.Application.DTOs.ClientCommentDto;
using FluentValidation;

namespace BankAPI.Application.Validators.CommentValidators;

public class ClientCommentCreateValidator : AbstractValidator<ClientCommentCreateDto>
{
    public ClientCommentCreateValidator()
    {
        RuleFor(comment => comment.Text)
            .NotEmpty()
            .WithMessage("Comment text cannot be empty.")
            .MaximumLength(500)
            .WithMessage("Comment text must not exceed 500 characters.");
        
        RuleFor(comment => comment.UserId)
            .GreaterThan(0)
            .WithMessage("Invalid user id.");
        
        RuleFor(comment => comment.ClientId)
            .GreaterThan(0)
            .WithMessage("Invalid client id.");
    }
}