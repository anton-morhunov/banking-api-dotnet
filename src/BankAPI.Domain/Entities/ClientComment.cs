namespace BankAPI.Domain.Entities;

public class ClientComment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public UserModel User { get; set; } = null!;
    public int ClientId { get; set; }
    public ClientModel Client { get; set; } = null!;
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}