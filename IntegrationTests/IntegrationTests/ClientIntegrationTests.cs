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

        response.EnsureSuccessStatusCode();

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