namespace BankAPI.Application.DTOs.TransferDto;

public class TransferResponseDto
{
    public Guid TransferId { get; set; } =  Guid.NewGuid();
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; } =  DateTime.UtcNow;
    public string? Description { get; set; }
    public int SourceAccountId { get; set; }
    public int DestinationAccountId { get; set; }
    public decimal Amount { get; set; }
}