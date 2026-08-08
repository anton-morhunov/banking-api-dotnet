namespace BankAPI.Application.DTOs.ClientCommentDto;

public class ClientCommentResponseDto
{
    public int CommentId { get; set; }
    public string Text { get; set; } = string.Empty;
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int ClientId  { get; set; }
}