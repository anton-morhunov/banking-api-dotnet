namespace BankAPI.Application.DTOs.AccountCommentDto;

public class AccountCommentCreateDto
{
    public string Text { get; set; } = string.Empty;
    public int AccountId { get; set; }
    public int UserId { get; set; }
}