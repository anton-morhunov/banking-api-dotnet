using BankAPI.Domain.Entities;

namespace BankAPI.Application.Interfaces.RepositoryInterfaces.Clients;

public interface IClientRepository
{
    Task<ClientModel> AddClient(ClientModel client);
    Task <List<ClientModel>> GetAllClients();
    Task<ClientModel?> GetClientByIdAsync(int id);
    Task<ClientModel?> GetClientByName(string name);
    Task SaveAsync();
    Task<ClientModel?> GetClientByEmail(string email);

}