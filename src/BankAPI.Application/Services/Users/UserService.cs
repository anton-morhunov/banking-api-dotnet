using BankAPI.Application.DTOs.AuthDto;
using BankAPI.Application.Exceptions;
using BankAPI.Application.Interfaces.RepositoryInterfaces.Users;
using BankAPI.Application.Interfaces.ServiceInterfaces.Users;
using BankAPI.Application.Mappers;
using Microsoft.Extensions.Logging;

namespace BankAPI.Application.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;

    public UserService(
        IUserRepository userRepository, 
        ILogger<UserService> logger
        )
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<UserResponse?>> GetAllUsersAsync()
    {
        _logger.LogInformation(
            "Getting all users"
            );
        
        var users = await _userRepository.GetAllUsersAsync();
        
        return users.Select(UserMapper.ToResponseDto).ToList();
    }

    public async Task<UserResponse> GetUserByIdAsync(int id)
    {
        _logger.LogInformation(
            "Getting user with Id {Id}", 
            id
            );

        var user = await _userRepository.GetUserByIdAsync(id);

        if (user == null)
        {
            _logger.LogInformation(
                "There is no user with Id {Id}", 
                id
                );
            
            throw new NotFoundException($"User with Id {id} not found");
        }

        var response = UserMapper.ToResponseDto(user);
        
        _logger.LogInformation(
            "User with Id {Id} retrieved successfully", 
            user.Id
            );
        
        return response;
    }
}