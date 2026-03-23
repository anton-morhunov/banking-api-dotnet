using BankAPI.DTO.ClientDTO;
using BankAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientsControllers : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsControllers(
        IClientService clientService)
    {
        _clientService = clientService;
    }
    
    [HttpGet("debug")]
    public IActionResult Debug()
    {
        var claims = User.Claims.Select(c => new
        {
            c.Type,
            c.Value
        });

        return Ok(claims);
    }
    //Get Client by ID
    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClientResponseDTO>> GetClientById(int id)
    {
        var client = await _clientService.GetClientByIdAsync(id);
        
        if (client is null)
        {
            return NoContent();
        }
        
        return Ok(client);
    }
    
    //Create new Client
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<ClientResponseDTO>> CreateClient(ClientCreateDTO dto)
    {
        var client = await _clientService.CreateClientAsync(dto);
        
        return CreatedAtAction(nameof(GetClientById), 
            new { id = client.Id }, 
            client
            );
    }
    
    //Get all clients
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientResponseDTO>>> GetAllClientsAsync()
    {
        var clients = await _clientService.GetAllClientsAsync();

        if (!clients.Any())
        {
            return NotFound();
        }
        
        return Ok(clients);
    }

    //Get Client by name
    [HttpGet("name/{name}")]
    public async Task<ActionResult<List<ClientResponseDTO>>> GetClientByName([FromQuery] string? name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            var client = await _clientService.GetClientByNameAsync(name);
            return Ok(client);
        }

        var allClients = await _clientService.GetAllClientsAsync();
        return Ok(allClients);
        
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateClient(
        [FromRoute] int id, 
        [FromBody] ClientUpdateDTO dto)
    {
        var client = await _clientService.UpdateClientAsync(id, dto);

        if (client is null)
        {
            return NotFound();
        }
        
        return Ok(client);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> ClientUpdateStatus(int id, ClientStatusDTO dto)
    {
        var result = await _clientService.ClientUpdateStatusAsync(id, dto.Status);

        if (!result)
        {
            return NotFound();
        }
        
        return NoContent();
    }

    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<ClientResponseDTO>>> GetAllActiveClients()
    {
        var result = await _clientService.GetActiveСlientsAsync();
        
        return Ok(result);
    }
}