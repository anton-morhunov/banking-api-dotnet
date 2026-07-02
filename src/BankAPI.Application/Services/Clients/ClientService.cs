using BankAPI.Application.DTOs.ClientDto;
using BankAPI.Application.Exceptions;
using BankAPI.Application.Interfaces.ServiceInterfaces.Clients;
using BankAPI.Domain.Enums;
using BankAPI.Domain.Entities;
using BankAPI.Application.Interfaces.RepositoryInterfaces.Clients;
using Microsoft.Extensions.Logging;

namespace BankAPI.Application.Services.Clients;

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
        
        var existingClient = await _clientRepository.GetClientByEmail(normalizeEmail);

        if (clientModel.Email == existingClient?.Email)
        {
            _logger.LogInformation(
                "Client with email {clientEmail} already exists", 
                clientModel.Email
            );
            
            throw new ConflictException($"Client with email {clientModel.Email} already exists");
        }
        
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
            "Client {ClientId} created", 
            createdClient.Id
            );
        
        return response;
    }

    public async Task<ClientResponseDTO> GetClientByIdAsync(int id)
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
            
            throw new NotFoundException($"Client {id} not found");
        }

        var response = new ClientResponseDTO
        {
            Id = client.Id,
            Name = client.Name,
            Email = client.Email,
            PhoneNumber = client.PhoneNumber,
            Status = client.Status,
            Created = client.CreateDate
        };

        _logger.LogInformation(
            "Client {ClientId} was found", 
            id
            );
        
        return response;
    }

    public async Task<ClientResponseDTO> UpdateClientAsync(int id, ClientUpdateDTO clientUpdateDto)
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
            
            throw new NotFoundException($"Client {id} not found");
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
            Status = x.Status,
            Created = x.CreateDate
            
        }).ToList();
    }

    public async Task<ClientResponseDTO> GetClientByNameAsync(string name)
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
            
            throw new NotFoundException($"Client with Name {name} not found");
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