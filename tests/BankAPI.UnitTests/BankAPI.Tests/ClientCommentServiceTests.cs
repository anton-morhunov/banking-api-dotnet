using BankAPI.Application.DTOs.ClientCommentDto;
using BankAPI.Application.Interfaces.RepositoryInterfaces;
using BankAPI.Application.Services;
using BankAPI.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;

namespace BankAPI.UnitTests.BankAPI.Tests;

public class ClientCommentServiceTests
{
    [Fact]
    public async Task CreateCommentAsync_ShouldReturnResponseDto_WhenCommentCreated()
    {
        var mockCommentRepository = new Mock<IClientCommentRepository>();
        var mockLogger = new Mock<ILogger<ClientCommentService>>();

        var createDto = new ClientCommentCreateDto
        {
            Text = "test",
            ClientId = 1,
            UserId = 1
        };

        var createdComment = new ClientComment
        {
            Id = 1,
            UserId = 1,
            Text = "test",
            ClientId = 1,
            CreatedAt = DateTime.Now
        };
        
        mockCommentRepository
            .Setup(x => x.CreateCommentAsync(It.IsAny<ClientComment>()))
            .ReturnsAsync(createdComment);
        
        var service = new ClientCommentService(mockCommentRepository.Object, mockLogger.Object);

        var result = await service.CreateCommentAsync(createDto);
        
        Assert.NotNull(result);
        Assert.Equal(createdComment.Id, result.CommentId);
        Assert.Equal(createdComment.UserId, result.UserId);
        Assert.Equal(createdComment.Text, result.Text);
        Assert.Equal(createdComment.ClientId, result.ClientId);
        Assert.Equal(createdComment.CreatedAt, result.CreatedAt);
        
        mockCommentRepository
            .Verify(x => x.CreateCommentAsync(It.IsAny<ClientComment>()), 
                Times.Once);
    }

    [Fact]
    public async Task GetCommentByClientId_ShouldReturnResponseDto_WhenCommentExists()
    {
        var mockCommentRepository = new Mock<IClientCommentRepository>();
        var mockLogger = new Mock<ILogger<ClientCommentService>>();

        var comments = new List<ClientComment>
        {
            new ClientComment()
            {
                Id = 1,
                Text = "test",
                ClientId = 1,
                UserId = 1,
                CreatedAt = DateTime.Now
            },
            new ClientComment()
            {
                Id = 2,
                Text = "test2",
                ClientId = 1,
                UserId = 1,
                CreatedAt = DateTime.Now
            },
        };
        
        mockCommentRepository.Setup(x=>x.GetCommentsByClientIdAsync(It.IsAny<int>()))
            .ReturnsAsync(comments);
        
        var service = new ClientCommentService(mockCommentRepository.Object, mockLogger.Object);
        
        var result = await service.GetCommentsByClientIdAsync(1);
        
        Assert.NotNull(result); 
        
        Assert.Equal(2, result.Count);
        
        Assert.Equal(comments[0].Id, result[0].CommentId);
        Assert.Equal(comments[1].Id, result[1].CommentId);
        
        Assert.Equal(comments[0].Text, result[0].Text);
        Assert.Equal(comments[1].Text, result[1].Text);
        
        mockCommentRepository
            .Verify(x => x.GetCommentsByClientIdAsync(1), 
                Times.Once);
    }
}