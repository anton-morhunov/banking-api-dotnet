using bank_desktop.DTOs.Clients;

namespace bank_desktop.src.Services.Interfaces;

public interface IClientService
{
    Task<List<ClientResponseDto>> GetAllClientsAsync();
}