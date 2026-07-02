using BankAPI.Application.DTOs.AccountCommentDto;
using BankAPI.Application.Exceptions;
using BankAPI.Application.Interfaces.RepositoryInterfaces.Comments;
using BankAPI.Application.Interfaces.ServiceInterfaces.Comments;
using BankAPI.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BankAPI.Application.Services.Comments;

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
        _logger.LogInformation(
            "Creating comment for account {accountId} with content {commentContent}", 
            accountCommentCreateDto.AccountId, 
            accountCommentCreateDto.Text
        );
        
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
        
        _logger.LogInformation(
            "A new comment with {id} has been created",  
            createdComment.Id
        );
        
        return response;
    }

    public async Task<bool> DeleteAccountCommentAsync(int commentId)
    {
        _logger.LogDebug(
            "Deleting a comment"
        );
        
        var comment = await _accountCommentRepository.GetAccountCommentByIdAsync(commentId);
        
        _logger.LogInformation(
            "Looking for a comment with id {id}", 
            commentId
        );

        if (comment is null)
        {
            _logger.LogWarning(
                "Comment with id {id} was not found", 
                commentId
            );
            
            return false;
        }

        await _accountCommentRepository.DeleteAccountCommentAsync(comment);
        
        _logger.LogInformation(
            "Comment with id {id} deleted", 
            commentId
        );
        
        return true;
    }

    public async Task<List<AccountCommentResponseDto>> GetCommentsByAccountIdAsync(int accountId)
    {
        _logger.LogDebug(
            "Getting comments for a account with id {accountId}", 
            accountId
        );
        
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
        
        _logger.LogInformation(
            "Got  {count} comments for a account with id {accountId}", 
            comments.Count, 
            accountId
        );
        
        return response;
    }

    public async Task<AccountCommentResponseDto> UpdateAccountCommentAsync(
        int commentId,
        AccountCommentUpdateDto accountCommentUpdateDto
    )
    {
        
        _logger.LogInformation(
            "Getting a comment with id {id}", 
            commentId
        );
        
        var comment = await _accountCommentRepository.GetAccountCommentByIdAsync(commentId);

        if (comment is null)
        {
            _logger.LogWarning(
                "Comment with id {id} was not found", 
                commentId
            );
            
            throw new NotFoundException($"Comment with Id {commentId} not found");
        }
        
        comment.Text = accountCommentUpdateDto.Text ?? comment.Text;

        await _accountCommentRepository.SaveChangesAsync();
        
        _logger.LogInformation(
            "Comment with id {id} updated", 
            commentId
        );

        return new AccountCommentResponseDto
        {
            Text = accountCommentUpdateDto.Text,
            UpdatedAt = DateTime.UtcNow
        };
    }
}