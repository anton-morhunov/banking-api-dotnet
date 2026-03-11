using BankAPI.Enum;

namespace BankAPI.DTO.Auth;

public class UserResponse
{
    public int Id { get; set; }
    public string Email { get; set; }
    public UserRole UserRole { get; set; }
}