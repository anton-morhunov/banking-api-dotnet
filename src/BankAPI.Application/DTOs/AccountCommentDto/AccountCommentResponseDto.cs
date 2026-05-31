namespace BankAPI.Application.DTOs.AccountCommentDto;

public class AccountCommentResponseDto
{
    public string? Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CommentId { get; set; }
    public int UserId { get; set; }
    public int AccountId { get; set; }
    public DateTime? UpdatedAt { get; set; }
}