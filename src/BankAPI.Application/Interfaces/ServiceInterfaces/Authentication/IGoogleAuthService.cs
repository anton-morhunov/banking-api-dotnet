using BankAPI.Application.DTOs.AuthDto;

namespace BankAPI.Application.Interfaces.ServiceInterfaces.Authentication;

public interface IGoogleAuthService
{
    Task<AuthResponseDto> GoogleLoginAsync(string credential);
}