using BankAPI.Application.DTOs.AccountCommentDto;
using BankAPI.Domain.Entities;

namespace BankAPI.Application.Interfaces.ServiceInterfaces.Comments;

public interface IAccountCommentService
{
    Task<AccountCommentResponseDto> CreateAccountCommentAsync(AccountCommentCreateDto accountCommentCreateDto);
    Task<bool>  DeleteAccountCommentAsync(int commentId);
    Task<List<AccountCommentResponseDto>> GetCommentsByAccountIdAsync(int accountId);
    Task<AccountCommentResponseDto?> UpdateAccountCommentAsync(int commentId, AccountCommentUpdateDto accountCommentUpdateDto);
}