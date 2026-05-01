using BankAPI.Application.DTOs.ClientDto;
using BankAPI.Application.Interfaces.ServiceInterfaces;
using BankAPI.Domain.Enums;
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
    
    /*[HttpGet("debug")]
    public IActionResult Debug()
    {
        var claims = User.Claims.Select(c => new
        {
            c.Type,
            c.Value
        });

        return Ok(claims);
    }*/
    
    //Get Client by ID
    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClientResponseDTO>> GetClientById(int id)
    {
        var client = await _clientService.GetClientByIdAsync(id);
        
        if (client is null)
        {
            return NotFound();
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
    
    [Authorize (Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientResponseDTO>>> GetClientsAsync([FromQuery] string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            var clients = await _clientService.GetAllClientsAsync();
            return Ok(clients);
        }

        var client = await _clientService.GetClientByNameAsync(name);
        return Ok(client);
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
    public async Task<IActionResult> ClientUpdateStatus(int id, ClientStatus dto)
    {
        var result = await _clientService.ClientUpdateStatusAsync(id, dto);

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