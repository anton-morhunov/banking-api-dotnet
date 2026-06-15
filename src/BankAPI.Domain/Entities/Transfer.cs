using BankAPI.Domain.Enums;

namespace BankAPI.Domain.Entities;

public class Transfer
{
    public Guid TransferId { get; set; } = Guid.NewGuid();
    public int UserId { get; set; }
    public int SourceAccountId { get; set; }
    public int DestinationAccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; } =  DateTime.UtcNow;
    public string? Description { get; set; }
    public TransferStatus TransferStatus { get; set; }
    public UserModel User { get; set; } = null!;
    public AccountModel SourceAccount { get; set; } = null!;
    public AccountModel DestinationAccount { get; set; } = null!;
}