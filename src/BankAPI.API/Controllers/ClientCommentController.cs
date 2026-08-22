using BankAPI.Application.DTOs.ClientCommentDto;
using BankAPI.Application.Interfaces.ServiceInterfaces.Comments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;

namespace BankAPI.Controllers;

[EnableRateLimiting("user-limit")]
[ApiController]
[Route("api/comments")]

public class ClientCommentController : ControllerBase
{
    private readonly IClientCommentService  _clientCommentService;
    
    public ClientCommentController(IClientCommentService clientCommentService)
    {
        _clientCommentService = clientCommentService;
    }
    
    [Authorize(Roles = "Admin, Employee")]
    [HttpPost]
    public async Task<ActionResult<ClientCommentResponseDto>> CreateCommentAsync(ClientCommentCreateDto clientCommentCreateDto)
    {
        var comment = await _clientCommentService.CreateCommentAsync(clientCommentCreateDto);
        
        return Ok(comment);
    }
    
    [Authorize(Roles = "Admin, Employee")]
    [HttpDelete]
    public async Task<ActionResult> DeleteCommentAsync(int commentId)
    { 
        await _clientCommentService.DeleteCommentAsync(commentId);
        
        return Ok();
    }
    
    [Authorize(Roles = "Admin, Employee")]
    [HttpGet("client/{clientId:int}")]
    public async Task<ActionResult<IEnumerable<ClientCommentResponseDto>>> GetAllCommentsByClientIdAsync(int clientId)
    {
        var response = await _clientCommentService.GetCommentsByClientIdAsync(clientId);
        
        return Ok(response);
    }
    
    [Authorize(Roles = "Admin, Employee")]
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