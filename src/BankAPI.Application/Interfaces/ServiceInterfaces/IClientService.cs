using BankAPI.Application.DTOs.ClientDto;
using BankAPI.Domain.Enums;

namespace BankAPI.Application.Interfaces.ServiceInterfaces;

public interface IClientService
{
    Task<bool> ClientUpdateStatusAsync(int id, ClientStatus status);
    Task<List<ClientResponseDTO>> GetActiveСlientsAsync();
    Task<ClientResponseDTO> CreateClientAsync(ClientCreateDTO clientCreateDto);
    Task<ClientResponseDTO?> GetClientByIdAsync(int id);
    Task<ClientResponseDTO?> UpdateClientAsync(int id, ClientUpdateDTO clientUpdateDto);
    Task<IEnumerable<ClientResponseDTO>> GetAllClientsAsync();
    Task<ClientResponseDTO?> GetClientByNameAsync(string name);
}