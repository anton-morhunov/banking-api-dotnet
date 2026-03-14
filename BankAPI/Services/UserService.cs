using BankAPI.DTO.Auth;
using BankAPI.Repositories.Interfaces;
using BankAPI.Services.Interfaces;

namespace BankAPI.Services;

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
        
        return users.Select(x => new UserResponse
        {
            Id = x.Id,
            Email = x.Email,
            UserRole = x.Role
            
        }).ToList();
    }

    public async Task<UserResponse?> GetUserByIdAsync(int id)
    {
        _logger.LogInformation(
            "Gettin user with Id {Id}", 
            id
            );

        var user = await _userRepository.GetUserByIdAsync(id);

        if (user == null)
        {
            _logger.LogInformation(
                "There is no user with Id {Id}", 
                id
                );
            
            return null;
        }

        var response = new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            UserRole = user.Role
        };
        
        _logger.LogInformation(
            "User with Id {Id} retrieved successfully", 
            user.Id
            );
        
        return response;
    }
}