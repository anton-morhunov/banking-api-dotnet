using BankAPI.Domain.Enums;

namespace BankAPI.Application.DTOs.AuthDto;

public class UserResponse
{
    public int Id { get; set; }
    public string Email { get; set; }
    public UserRole UserRole { get; set; }
}