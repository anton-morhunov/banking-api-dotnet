using BankAPI.Application.DTOs.ClientCommentDto;
using FluentValidation;

namespace BankAPI.Application.Validators.CommentValidators;

public class ClientCommentUpdateValidator : AbstractValidator<ClientCommentUpdateDto>
{
    public ClientCommentUpdateValidator()
    {
        
    }
}