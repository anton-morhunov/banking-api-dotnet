using BankAPI.Application.DTOs.AccountCommentDto;
using BankAPI.Application.Interfaces.RepositoryInterfaces;
using BankAPI.Application.Interfaces.ServiceInterfaces;
using BankAPI.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BankAPI.Application.Services;

public class AccountCommentService : IAccountCommentService
{
    private readonly IAccountCommentRepository _accountCommentRepository;
    private readonly ILogger<AccountCommentService> _logger;

    public AccountCommentService(
        IAccountCommentRepository accountCommentRepository,  
        ILogger<AccountCommentService> logger
        )
    {
        _accountCommentRepository = accountCommentRepository;
        _logger = logger;
    }

    public async Task<AccountCommentResponseDto> CreateAccountCommentAsync(AccountCommentCreateDto accountCommentCreateDto)
    {
        AccountComment commentDto = new AccountComment
        {
            Text = accountCommentCreateDto.Text,
            UserId = accountCommentCreateDto.UserId,
            AccountId = accountCommentCreateDto.AccountId,
            CreatedAt = DateTime.UtcNow,
        };

        var createdComment = await _accountCommentRepository.CreateAccountCommentAsync(commentDto);

        var response = new AccountCommentResponseDto
        {
            CreatedAt = createdComment.CreatedAt,
            Text = createdComment.Text,
            UserId = createdComment.UserId,
            AccountId = createdComment.AccountId,
            CommentId = createdComment.Id
        };
        
        return response;
    }

    public async Task<bool> DeleteAccountCommentAsync(int commentId)
    {
        var comment = await _accountCommentRepository.GetAccountCommentByIdAsync(commentId);

        if (comment is null)
        {
            return false;
        }

        await _accountCommentRepository.DeleteAccountCommentAsync(comment);
        
        return true;
    }

    public async Task<List<AccountCommentResponseDto>> GetCommentsByAccountIdAsync(int accountId)
    {
        var comments = await _accountCommentRepository.GetCommentsByAccountIdAsync(accountId);

        var response = new List<AccountCommentResponseDto>();

        foreach (var comment in comments)
        {
            var dto = new AccountCommentResponseDto
            {
                CreatedAt = comment.CreatedAt,
                Text = comment.Text,
                CommentId = comment.Id,
                AccountId = comment.AccountId,
            };
            
            response.Add(dto); 
        }
        
        return response;
    }

    public async Task<AccountCommentResponseDto?> UpdateAccountCommentAsync(
        int commentId,
        AccountCommentUpdateDto accountCommentUpdateDto
    )
    {
        var comment = await _accountCommentRepository.GetAccountCommentByIdAsync(commentId);

        if (comment is null)
        {
            return null;
        }
        
        comment.Text = accountCommentUpdateDto.Text ?? comment.Text;

        await _accountCommentRepository.SaveChangesAsync();

        return new AccountCommentResponseDto
        {
            Text = accountCommentUpdateDto.Text,
            UpdatedAt = DateTime.UtcNow
        };
    }
}