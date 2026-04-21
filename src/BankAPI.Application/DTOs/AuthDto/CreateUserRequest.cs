using BankAPI.Domain.Enums;

namespace BankAPI.Application.DTOs.AuthDto;

public class CreateUserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRole UserRole { get; set; }
}