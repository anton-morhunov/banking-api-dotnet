using System.Net.Http.Json;
using BankAPI.Application.DTOs.ClientDto;

namespace BankAPI.IntegrationTests.IntegrationTests;

public class ClientIntegrationTests 
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ClientIntegrationTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetClientsAsync_ShouldReturnClients()
    {
        var response = await _client.GetAsync("/api/clients");

        var raw = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Status: {response.StatusCode}\nBody: {raw}");
        }

        Assert.True(response.IsSuccessStatusCode, raw);

        var content = await response.Content.ReadFromJsonAsync<List<ClientResponseDTO>>();

        Assert.NotNull(content);
        Assert.NotEmpty(content);

        Assert.Contains(content, c => 
            c.Name == "Anton" && 
            c.Email == "anton@test.com" &&
            c.PhoneNumber == "123456789"
        );
    }
    /*[Fact]
    public async Task ClientUpdateStatusAsync_ShouldReturnClient()
    {
        var updateRequest = new
        {
            name = "Tets",
            email = "test@test.com",
            phoneNumber = "123456789"
        };
        
        var response = await _client.PatchAsJsonAsync("/api/clients/1", updateRequest);
        response.EnsureSuccessStatusCode();
        
        var getResponse = await _client.GetAsync("/api/clients/1");
        var raw = await getResponse.Content.ReadFromJsonAsync<List<ClientResponseDTO>>();
        
        var updatedClient = raw.First(c => c.Id == 1);
        
        Assert.Equal("Tets", updatedClient.Name);
        Assert.Equal("test@test.com", updatedClient.Email);
        Assert.Equal("123456789", updatedClient.PhoneNumber);
    }*/
}