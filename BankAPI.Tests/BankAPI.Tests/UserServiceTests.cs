using BankAPI.Enum;
using BankAPI.Repositories.Interfaces;
using BankAPI.Services;
using Microsoft.Extensions.Logging;
using Moq;
using BankAPI.Models;

namespace BankAPI.Tests;

public class UserServiceTests
{
    [Fact]
    public async Task GetAllUsers_ShouldReturnResponse_WhenUsersWasFound()
    {
        var mockUserRepository = new Mock<IUserRepository>();
        var mockLoger = new Mock<ILogger<UserService>>();

        var userModel = new List<UserModel>
        {
            new UserModel()
            {
                Id = 1, 
                Email = "testUser1@gmail.com", 
            },
            new UserModel()
            {
                Id = 2, 
                Email = "testUser2@gmail.com", 
            }
        };
        
        mockUserRepository.Setup(x => x.GetAllUsersAsync())
            .ReturnsAsync(userModel);
        
        var service = new UserService(mockUserRepository.Object, mockLoger.Object);
        
        var result = await service.GetAllUsersAsync();
        
        var resultList = result.ToList();
        
        Assert.NotNull(result);
        Assert.Equal(2, resultList.Count);
        Assert.Equal("testUser1@gmail.com", resultList[0].Email);
        Assert.Equal("testUser2@gmail.com", resultList[1].Email);
    }
    
    [Fact]
    public async Task GetUserByIdAsync_ShouldReturnResponse_WhenUserWasFound()
    {
        var mockUserRepository = new Mock<IUserRepository>();
        var mockLoger = new Mock<ILogger<UserService>>();

        var clientId = 1;

        var userModel = new UserModel
        {
            Id = clientId,
            Email = "userTest1@gmail.com",
            Role = UserRole.Admin
            
        };
        
        mockUserRepository.Setup(x => x.GetUserByIdAsync(clientId))
            .ReturnsAsync(userModel);
        
        var service = new UserService(mockUserRepository.Object, mockLoger.Object);
        
        var result = await service.GetUserByIdAsync(clientId);
        
        Assert.NotNull(result);
        Assert.Equal(userModel.Id, result.Id);
        Assert.Equal(userModel.Email, result.Email);
        Assert.Equal(userModel.Role, result.UserRole);
        
        mockUserRepository
            .Verify(x => x.GetUserByIdAsync(
                    clientId), 
                Times.Once);
    }
}