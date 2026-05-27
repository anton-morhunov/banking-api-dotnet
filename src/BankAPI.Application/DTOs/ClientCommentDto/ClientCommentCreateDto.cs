namespace BankAPI.Application.DTOs.ClientCommentDto;

public class ClientCommentCreateDto
{
    public string Text { get; set; } = string.Empty;
    public int ClientId { get; set; }
    public int UserId { get; set; }
}