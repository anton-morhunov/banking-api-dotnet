using BankAPI.Domain.Enums;

namespace BankAPI.Domain.Entities;

public class UserModel
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public UserRole Role { get; set; }
    public DateTime CreatedAt { get; set; } =  DateTime.UtcNow;
    public ICollection<Deposit> Deposits { get; set; }  
        = new List<Deposit>();
    public ICollection<Transfer> Transfers { get; set; } 
        = new List<Transfer>();
}