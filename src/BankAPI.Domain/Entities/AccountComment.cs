namespace BankAPI.Domain.Entities;

public class AccountComment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public UserModel User { get; set; } = null!;
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public AccountModel Account { get; set; } = null!;
    public int AccountId { get; set; }
}