using bank_desktop.src.DTOs.Requests;
using bank_desktop.src.DTOs.Responses;

namespace bank_desktop.src.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
    }
}
