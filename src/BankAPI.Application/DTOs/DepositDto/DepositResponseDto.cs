namespace BankAPI.Application.DTOs.DepositDto;

public class DepositResponseDto
{
    public Guid DepositId { get; set; }
    public decimal Amount { get; set; }
    public int AccountId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; } =  DateTime.UtcNow;
}