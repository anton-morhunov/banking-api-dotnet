using BankAPI.Application.DTOs.AccountCommentDto;
using BankAPI.Application.Interfaces.RepositoryInterfaces.Comments;
using BankAPI.Application.Services.Comments;
using BankAPI.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;

namespace BankAPI.UnitTests.BankAPI.Tests;

public class AccountCommentServiceTests
{
    [Fact]
    public async Task CreateCommentAsync_ShouldReturnDto_WhenCommentCreated()
    {
        var mockAccountRepository = new Mock<IAccountCommentRepository>();
        var mockLogger = new Mock<ILogger<AccountCommentService>>();

        var createDto = new AccountCommentCreateDto
        {
            Text = "test_comment",
            AccountId = 1,
            UserId = 1
        };

        var createdComment = new AccountComment
        {
            AccountId = 1,
            Text = "test_comment",
            UserId = 1,
            Id = 1,
            CreatedAt = DateTime.UtcNow
        };

        mockAccountRepository
            .Setup(x => 
                x.CreateAccountCommentAsync(It.IsAny<AccountComment>()))
            .ReturnsAsync(createdComment);
        
        var service = new AccountCommentService(mockAccountRepository.Object, mockLogger.Object);
        
        var result = await service.CreateAccountCommentAsync(createDto);
        
        Assert.NotNull(result);
        Assert.Equal(createdComment.Text, result.Text);
        Assert.Equal(createdComment.AccountId, result.AccountId);
        Assert.Equal(createdComment.UserId, result.UserId);
        Assert.Equal(createdComment.Id, result.CommentId);
        Assert.Equal(createdComment.CreatedAt, result.CreatedAt);
        
        mockAccountRepository
            .Verify(x => x.CreateAccountCommentAsync(It.IsAny<AccountComment>()), 
                Times.Once);
    }
    
    [Fact]
    public async Task DeleteCommentAsync_ShouldDeleteComment_WhenCommentDeleted()
    {
        var mockAccountRepository = new Mock<IAccountCommentRepository>();
        var mockLogger = new Mock<ILogger<AccountCommentService>>();

        AccountComment accountComment = new AccountComment
        {
            AccountId = 1,
            Text = "test_comment",
            UserId = 1,
            Id = 1
        };

       mockAccountRepository
           .Setup(x => 
               x.GetAccountCommentByIdAsync(accountComment.Id))
           .ReturnsAsync(accountComment);

       mockAccountRepository
           .Setup(x => 
               x.DeleteAccountCommentAsync(accountComment));
        
        var service = new AccountCommentService(mockAccountRepository.Object, mockLogger.Object);
        
        var result =  await service.DeleteAccountCommentAsync(accountComment.Id);
        
        Assert.True(result);
        
        mockAccountRepository
            .Verify(x 
                => x.GetAccountCommentByIdAsync(1), 
                Times.Once);
    }

    [Fact]
    public async Task GetAccountCommentByIdAsync_ShouldReturnDto_WhenCommentsExists()
    {
        var mockAccountRepository = new Mock<IAccountCommentRepository>();
        var mockLogger = new Mock<ILogger<AccountCommentService>>();

        var comments = new List<AccountComment>
        {
            new AccountComment
            {
                AccountId = 1,
                Text = "test_comment",
                Id = 1,
                CreatedAt = DateTime.UtcNow
            },
            
            new AccountComment
            {
                AccountId = 1,
                Text = "test_comment2",
                Id = 2,
                CreatedAt = DateTime.UtcNow
            }
        };

        mockAccountRepository
            .Setup(x => 
                x.GetCommentsByAccountIdAsync(1))
            .ReturnsAsync(comments);
        
        var service = new AccountCommentService(mockAccountRepository.Object, mockLogger.Object);

        var result = await service.GetCommentsByAccountIdAsync(1);
        
        Assert.NotNull(result);
        
        Assert.Equal(2, result.Count);
        
        Assert.Equal(comments[0].AccountId, result[0].AccountId);
        Assert.Equal(comments[1].AccountId, result[1].AccountId);
        
        Assert.Equal(comments[0].Text, result[0].Text);
        Assert.Equal(comments[1].Text, result[1].Text);
        
        Assert.Equal(comments[0].Id, result[0].CommentId);
        Assert.Equal(comments[1].Id, result[1].CommentId);
    }
}