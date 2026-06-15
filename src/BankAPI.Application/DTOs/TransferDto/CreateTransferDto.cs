namespace BankAPI.Application.DTOs.TransferDto;

public class CreateTransferDto
{
    public int UserId { get; set; }
    public string? Description { get; set; }
    public int SourceAccountId { get; set; }
    public int DestinationAccountId { get; set; }
    public decimal Amount { get; set; }
}