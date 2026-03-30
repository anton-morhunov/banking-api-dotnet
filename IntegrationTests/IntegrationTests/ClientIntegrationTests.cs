using System.Net.Http.Json;
using BankAPI.DTO.ClientDTO;


namespace IntegrationTests;

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
}