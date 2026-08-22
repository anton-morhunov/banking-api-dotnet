using BankAPI.Application.DTOs.AccountDto;
using BankAPI.Application.Interfaces.ServiceInterfaces.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BankAPI.Controllers;

[EnableRateLimiting("user-limit")]
[ApiController]
[Route("api/accounts")]

public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(
        IAccountService accountService
        )
    {
        _accountService = accountService;
    }
    
    [Authorize(Roles = "Admin, Employee")]
    [HttpGet ("{accountId:int}")]
    public async Task<ActionResult<AccountResponseDto>> GetAccountById(
        int? accountId
        )
    { 
        var account = await _accountService.GetAccountByIdAsync(
            accountId 
            ); 
        
        if (account is null) 
        {
            return NotFound();
        }
            
        return Ok(account);
    }
    
    [Authorize(Roles = "Admin, Employee")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountResponseDto>>> GetAllAccounts()
    {
        var all = await _accountService.GetAllAccounts();
        return Ok(all);
    }
    
    [Authorize(Roles = "Admin, Employee")]
    [HttpGet("client/{clientId:int}")]
    public async Task<ActionResult<IEnumerable<AccountResponseDto>>> GetAllAccountsByClientId(int clientId)
    {
        var accounts = await _accountService.GetAllAccountsByClientIdAsync(clientId);
        
        return Ok(accounts);
    }
    
    [Authorize(Roles = "Admin, Employee")]
    [HttpPost]
    public async Task<ActionResult<AccountResponseDto>> CreateAccount(AccountCreateDto accountCreateDto)
    {
        var account = await _accountService.CreateAccount(accountCreateDto);
        
        return CreatedAtAction(
            nameof(GetAccountById),
            new { accountId = account.AccountId }, 
            account
            );
    }
    
    [Authorize(Roles = "Admin, Employee")]
    [HttpPatch("{accountId:int}/status")]
    public async Task<ActionResult<AccountResponseDto>> AccountUpdateStatusAsync(
        int accountId, 
        AccountUpdateDto accountUpdateDto
        )
    {
        var updateAccount = await _accountService.AccountUpdateStatusAsync(
            accountId, 
            accountUpdateDto
            );

        if (updateAccount is null)
        {
            return NotFound();
        }
        
        return Ok(updateAccount);
    }

    /*[Authorize(Roles = "Admin, Employee")]
    [HttpPatch("{id:int}/close")]
    public async Task<ActionResult<bool>> CloseAccountAsync(
        int accountId, 
        int clientId
        )
    {
        var closeAccount = await _accountService.CloseAccountAsync(accountId
            //clientId
            );

        return Ok(closeAccount);
    }*/
    
    [Authorize(Roles = "Admin, Employee")]
    [HttpPatch("{accountId:int}/plan")]
    public async Task<ActionResult<AccountResponseDto>> UpdatePlanAsync(
        int accountId,
        AccountUpdateDto accountUpdateDto
        )
    {
        var updateAccountPlan = await _accountService.AccountUpdatePlanAsync(
            accountId, 
            accountUpdateDto
            );

        if (updateAccountPlan is null)
        {
            return NotFound();
        }
        return Ok(updateAccountPlan);
    }
}