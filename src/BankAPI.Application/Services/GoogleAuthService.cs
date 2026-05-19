using BankAPI.Application.DTOs.AuthDto;
using BankAPI.Application.Interfaces.RepositoryInterfaces;
using BankAPI.Application.Interfaces.ServiceInterfaces;
using Google.Apis.Auth;

namespace BankAPI.Application.Services;

public class GoogleAuthService : IGoogleAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public GoogleAuthService(
        IUserRepository userRepository, 
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }
    
    public async Task<AuthResponseDto> GoogleLoginAsync(string credential)
    {
        var payload = await GoogleJsonWebSignature.ValidateAsync(credential);
        
        var user = await _userRepository.GetUserByEmailAsync(payload.Email);

        if (user is null)
        {
            throw new UnauthorizedAccessException("User is not registered");
        }

        var token = _jwtService.GenerateToken(user);
        
        return new AuthResponseDto
        {
            Token = token,
            Email = user.Email,
            UserRole = user.Role
        };
    }
}