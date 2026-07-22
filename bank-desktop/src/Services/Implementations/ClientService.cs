using System.Net.Http;
using bank_desktop.DTOs.Clients;
using bank_desktop.src.Services.Base;
using bank_desktop.src.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace bank_desktop.src.Services.Implementations;

public class ClientService : BaseApiServices, IClientService
{
    public ClientService(
        HttpClient httpClient,
        IConfiguration configuration
    ) : base(httpClient, configuration)
    {
        
    }

    public async Task<List<ClientResponseDto>> GetAllClientsAsync()
    {
        return await GetAsync<List<ClientResponseDto>>("api/clients");
    }
}