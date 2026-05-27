using BankAPI.Application.DTOs.ClientCommentDto;
using BankAPI.Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers;

[ApiController]
[Route("api/comments")]

public class ClientCommentController : ControllerBase
{
    private readonly IClientCommentService  _clientCommentService;
    
    public ClientCommentController(IClientCommentService clientCommentService)
    {
        _clientCommentService = clientCommentService;
    }

    [HttpPost]
    public async Task<ActionResult<ClientCommentResponseDto>> CreateCommentAsync(ClientCommentCreateDto clientCommentCreateDto)
    {
        var comment = await _clientCommentService.CreateCommentAsync(clientCommentCreateDto);
        
        return Ok(comment);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteCommentAsync(int commentId)
    { 
        await _clientCommentService.DeleteCommentAsync(commentId);
        
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientCommentResponseDto>>> GetAllCommentsByClientIdAsync(int clientId)
    {
        var response = await _clientCommentService.GetCommentsByClientIdAsync(clientId);
        
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<ClientCommentResponseDto>> UpdateCommentAsync(int id,
        ClientCommentUpdateDto clientCommentUpdateDto)
    {
        var response = await _clientCommentService.UpdateCommentAsync(id, clientCommentUpdateDto);

        if (response == null)
        {
            return NotFound();
        }
        
        return Ok(response);
    }
}