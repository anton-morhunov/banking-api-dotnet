using BankAPI.Application.DTOs.AccountCommentDto;
using BankAPI.Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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

    [Authorize(Roles = "Admin, Employee")]
    [HttpPost]
    public async Task<ActionResult<AccountCommentResponseDto>> CreateCommentAsync(
        AccountCommentCreateDto accountCommentCreateDto)
    {
        var response = await _accountCommentService.CreateAccountCommentAsync(accountCommentCreateDto);
        return Ok(response);
    }

    [Authorize(Roles = "Admin, Employee")]
    [HttpDelete]
    public async Task<ActionResult> DeleteCommentAsync(int commentId)
    {
        var response = await _accountCommentService.DeleteAccountCommentAsync(commentId);
        
        return Ok(response);
    }

    [Authorize(Roles = "Admin, Employee")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountCommentResponseDto>>> GetAllCommentsAsync(int accountId)
    {
        var comments = await _accountCommentService.GetCommentsByAccountIdAsync(accountId);

        return Ok(comments);
    }

    [Authorize(Roles = "Admin, Employee")]
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