using BankAPI.Domain.Enums;

namespace BankAPI.Application.DTOs.AuthDto;

public class AuthResponseDto
{
    public string Token { get; set; }
    public string Email { get; set; }
    public UserRole UserRole{ get; set; }
}