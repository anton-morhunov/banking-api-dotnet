using BankAPI.Application.DTOs.AccountCommentDto;
using BankAPI.Domain.Entities;

namespace BankAPI.Application.Mappers;

public static class AccountCommentsMapper
{
    public static AccountCommentResponseDto ToResponseDto(AccountComment accountComment)
    {
        return new AccountCommentResponseDto
        {
            Text = accountComment.Text,
            CreatedAt = accountComment.CreatedAt,
            UpdatedAt = accountComment.UpdatedAt,
            AccountId = accountComment.AccountId,
            UserId = accountComment.UserId,
            CommentId = accountComment.Id
        };
    }

    public static AccountComment ToAccountCommentModel(AccountCommentCreateDto accountCommentCreateDto)
    {
        return new AccountComment
        {
            Text = accountCommentCreateDto.Text,
            UserId = accountCommentCreateDto.UserId,
            AccountId = accountCommentCreateDto.AccountId
        };
    }
}