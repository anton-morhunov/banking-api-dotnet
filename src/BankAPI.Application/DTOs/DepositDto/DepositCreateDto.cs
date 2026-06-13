namespace BankAPI.Application.DTOs.DepositDto;

public class DepositCreateDto
{
    public decimal Amount { get; set; }
    public int AccountId { get; set; }
    public int UserId { get; set; }
    public string? Description { get; set; } 
}