using BankAPI.Application.DTOs.ClientCommentDto;

namespace BankAPI.Application.Interfaces.ServiceInterfaces.Comments;

public interface IClientCommentService
{
    Task<ClientCommentResponseDto> CreateCommentAsync(ClientCommentCreateDto clientCommentCreateDto);
    Task<bool> DeleteCommentAsync(int commentId);
    Task<List<ClientCommentResponseDto>> GetCommentsByClientIdAsync(int clientId);
    Task<ClientCommentResponseDto?> UpdateCommentAsync(int commentId, ClientCommentUpdateDto clientCommentUpdateDto);
}