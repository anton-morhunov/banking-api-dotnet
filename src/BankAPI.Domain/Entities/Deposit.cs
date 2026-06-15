namespace BankAPI.Domain.Entities;

public class Deposit
{
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid DepositId { get; set; } =  Guid.NewGuid();
    public int ClientId { get; set; }
    public int AccountId { get; set; }
    public int UserId { get; set; }
    public AccountModel Account { get; set; } = null!;
    public UserModel User { get; set; } = null!;
}