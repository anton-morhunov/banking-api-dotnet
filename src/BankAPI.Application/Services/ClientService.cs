using BankAPI.Application.DTOs.ClientDto;
using BankAPI.Application.Interfaces.ServiceInterfaces;
using BankAPI.Domain.Enums;
using BankAPI.Domain.Entities;
using BankAPI.Application.Interfaces.RepositoryInterfaces;
using Microsoft.Extensions.Logging;

namespace BankAPI.Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly ILogger<ClientService> _logger;

    public ClientService(IClientRepository clientRepository , 
        ILogger<ClientService> logger)
    {
        _clientRepository = clientRepository;
        _logger = logger;
    }

    public async Task<bool> ClientUpdateStatusAsync(int id, ClientStatus status)
    {
        if (!Enum.IsDefined(typeof(ClientStatus), status))
        {
            _logger.LogWarning("Invalid status {Status} for client {ClientId}", status, id);
            return false;
        }

        var client = await _clientRepository.GetClientByIdAsync(id);

        if (client == null)
        {
            _logger.LogWarning("Client {ClientId} not found", id);
            return false;
        }

        if (client.Status == status)
        {
            _logger.LogInformation("Client {ClientId} already has status {Status}", id, status);
            return true;
        }

        var oldStatus = client.Status;
        client.Status = status;

        await _clientRepository.SaveAsync();

        _logger.LogInformation(
            "Client {ClientId} status changed from {OldStatus} to {NewStatus}",
            id,
            oldStatus,
            status
        );

        return true;
    }

    public async Task<List<ClientResponseDTO>> GetActiveСlientsAsync()
    {
        _logger.LogInformation(
            "Getting active clients"
            );
        
        var clients = await _clientRepository.GetAllClients();

        return  clients
            .Where(x => x.Status == ClientStatus.Active)
            .Select(x => new ClientResponseDTO
            {
                Id = x.Id,
                Name = x.Name,
                Status = x.Status,
                
            })
            .ToList();
    }

    public async Task<ClientResponseDTO> CreateClientAsync(ClientCreateDTO clientCreateDto)
    {
        _logger.LogInformation(
            "Creating new client"
            );
        
        var normalizeEmail = clientCreateDto.Email.Trim().ToLowerInvariant();
        
        ClientModel clientModel = new ClientModel
        {
            CreateDate = DateTime.UtcNow,
            Status = ClientStatus.Active,
            Accounts = new List<AccountModel>(),
            Name = clientCreateDto.Name.Trim(),
            Email = normalizeEmail,
            PhoneNumber = clientCreateDto.PhoneNumber.Trim()
        };

        var createdClient = await _clientRepository.AddClient(clientModel);

        var response = new ClientResponseDTO
        {
            Id = createdClient.Id,
            Name = createdClient.Name,
            Email = createdClient.Email,
            PhoneNumber = createdClient.PhoneNumber,
            Status = createdClient.Status
        };
        
        await _clientRepository.SaveAsync();
        
        _logger.LogInformation(
            "Created new client"
            );
        
        return response;
    }

    public async Task<ClientResponseDTO?> GetClientByIdAsync(int id)
    {
        _logger.LogInformation(
            "Getting client {ClientId}", 
            id
            );
        
        var client = await _clientRepository.GetClientByIdAsync(id);
        
        if(client == null)
        {
            _logger.LogWarning(
                "Client {ClientId} was not found", 
                id
                );
            
            return null;
        }

        var response = new ClientResponseDTO
        {
            Id = client.Id,
            Name = client.Name,
            Email = client.Email,
            PhoneNumber = client.PhoneNumber,
            Status = client.Status
        };

        _logger.LogInformation(
            "Client {ClientId} was found", 
            id
            );
        
        return response;
    }

    public async Task<ClientResponseDTO?> UpdateClientAsync(int id, ClientUpdateDTO clientUpdateDto)
    {
        _logger.LogInformation(
            "Updating client{ClientId}", 
            id
            );
        
        var  client = await _clientRepository.GetClientByIdAsync(id);

        if (client == null)
        {
            _logger.LogWarning(
                "Client {ClientId} was not found while updating information", 
                id
                );
            
            return null;
        }

        client.Name = clientUpdateDto.Name ?? client.Name;
        client.Email = clientUpdateDto.Email ?? client.Email;
        client.PhoneNumber = clientUpdateDto.PhoneNumber ?? client.PhoneNumber;
        
        await _clientRepository.SaveAsync();

        _logger.LogInformation(
            "Client {ClientId} was updated", 
            id
            );
        
        return new ClientResponseDTO
        {
            Name = client.Name,
            Email = client.Email,
            PhoneNumber = client.PhoneNumber,
        };
    }

    public async Task<IEnumerable<ClientResponseDTO>> GetAllClientsAsync()
    {
        _logger.LogInformation(
            "Getting all clients"
            );
        
        var clients = await _clientRepository.GetAllClients();
        
        return clients.Select(x => new ClientResponseDTO
        {
            Id = x.Id,
            Name = x.Name,
            Email = x.Email,
            PhoneNumber = x.PhoneNumber,
            Status = x.Status
            
        }).ToList();
    }

    public async Task<ClientResponseDTO?> GetClientByNameAsync(string name)
    {
        _logger.LogInformation(
            "Getting client {ClientName} by name", 
            name
            );
        
        var client = await _clientRepository.GetClientByName(name);

        if (client == null)
        {
            _logger.LogWarning(
                "Client {ClientName} was not found", 
                name
                );
            
            return null;
        }

        var response = new ClientResponseDTO
        {
            Id = client.Id,
            Name = client.Name,
            Email = client.Email,
            PhoneNumber = client.PhoneNumber,
            Status = client.Status
        };
        
        _logger.LogInformation(
            "Client with {ClientName} was found", 
            name
            );
        
        return response;
    }
}