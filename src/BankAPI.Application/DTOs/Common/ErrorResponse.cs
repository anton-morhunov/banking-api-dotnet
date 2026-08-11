namespace BankAPI.Application.DTOs.Common;

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; } =  string.Empty;
    public string Path { get; set; } =  string.Empty;
    public string TraceId { get; set; } =  string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string? StackTrace { get; set; }
}