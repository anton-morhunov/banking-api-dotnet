using BankAPI.Enum;

namespace BankAPI.Models;

public class UserModel
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public UserRole Role { get; set; }
}