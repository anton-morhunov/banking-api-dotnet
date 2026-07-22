using bank_desktop.src.DTOs.Requests;
using bank_desktop.src.DTOs.Responses;
using bank_desktop.src.Services.Base;
using bank_desktop.src.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace bank_desktop.src.Services.Implementations
{
    public class AuthService : BaseApiServices, IAuthService
    {
        public AuthService(
            HttpClient httpClient,
            IConfiguration configuration)
            : base(httpClient, configuration)
        {
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
           return await PostAsync<LoginRequestDto, LoginResponseDto>(
               "api/auth/login",
               request
               );
        }


    }
}
