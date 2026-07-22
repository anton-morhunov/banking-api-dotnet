namespace bank_desktop.DTOs.Clients;

public class ClientResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } =  string.Empty;
    public DateTime Created { get; set; } = DateTime.Now;
}