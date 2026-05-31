using BankAPI.Application.DTOs.AccountCommentDto;
using BankAPI.Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers;

[Route("api/account-comments")]
[ApiController]
public class AccountCommentController : ControllerBase
{
    private readonly IAccountCommentService _accountCommentService;
    
    public AccountCommentController(IAccountCommentService accountCommentService)
    {
        _accountCommentService = accountCommentService;
    }

    [HttpPost]
    public async Task<ActionResult<AccountCommentResponseDto>> CreateCommentAsync(
        AccountCommentCreateDto accountCommentCreateDto)
    {
        var response = await _accountCommentService.CreateAccountCommentAsync(accountCommentCreateDto);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteCommentAsync(int commentId)
    {
        var response = await _accountCommentService.DeleteAccountCommentAsync(commentId);
        
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountCommentResponseDto>>> GetAllCommentsAsync(int accountId)
    {
        var comments = await _accountCommentService.GetCommentsByAccountIdAsync(accountId);

        return Ok(comments);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAccountCommentAsync(
        int commendId,
        AccountCommentUpdateDto accountCommentUpdateDto
    )
    {
        var comment = await _accountCommentService.UpdateAccountCommentAsync(commendId, accountCommentUpdateDto);
        return Ok(comment);
    }
}