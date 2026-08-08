using BankAPI.Application.DTOs.ClientCommentDto;
using BankAPI.Domain.Entities;

namespace BankAPI.Application.Mappers;

public static class ClientCommentsMapper
{
    public static ClientCommentResponseDto ToResponseDto(ClientComment clientComment)
    {
        return new ClientCommentResponseDto
        {
            CommentId = clientComment.Id,
            Text = clientComment.Text,
            UserId = clientComment.UserId,
            CreatedAt = clientComment.CreatedAt,
            UpdatedAt = clientComment.UpdatedAt,
            ClientId = clientComment.ClientId,
        };
    }

    public static ClientComment ToClientCommentModel(ClientCommentCreateDto clientCommentCreateDto)
    {
        return new ClientComment
        {
            Text = clientCommentCreateDto.Text,
            ClientId = clientCommentCreateDto.ClientId,
            UserId = clientCommentCreateDto.UserId
        };
    }
}