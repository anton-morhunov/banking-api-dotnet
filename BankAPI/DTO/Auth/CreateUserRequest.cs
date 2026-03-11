using BankAPI.Enum;

namespace BankAPI.DTO.Auth;

public class CreateUserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRole UserRole { get; set; }
}