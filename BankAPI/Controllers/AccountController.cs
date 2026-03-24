using BankAPI.DTO.AccountDTO;
using BankAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers;

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

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AccountResponseDto>> GetAccountById(int accountId, int clientId)
    {
        var account = await _accountService.GetAccountByIdAsync(accountId,  clientId);

        if (account is null)
        {
            return NotFound();
        }
        
        return Ok(account);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountResponseDto>>> GetAllAccountsByClientId(int clientId)
    {
        var accounts = await _accountService.GetAllAccountsByClientIdAsync(clientId);
        
        return Ok(accounts);
    }
    
    [HttpPost]
    public async Task<ActionResult<AccountResponseDto>> CreateAccount(AccountCreateDto accountCreateDto)
    {
        var account = await _accountService.CreateAccount(accountCreateDto);
        
        return CreatedAtAction(
            nameof(GetAccountById),
            new { id = account.ClientId }, 
            account
            );
    }
    
    [HttpPatch("{id:int}/status")]
    public async Task<ActionResult<AccountResponseDto>> AccountUpdateStatusAsync(
        int accountId, 
        int clientId,
        AccountUpdateDto accountUpdateDto
        )
    {
        var updateAccount = await _accountService.AccountUpdateStatusAsync(
            accountId, 
            clientId, 
            accountUpdateDto
            );

        if (updateAccount is null)
        {
            return NotFound();
        }
        
        return Ok(updateAccount);
    }

    [HttpPatch("{id:int}/close")]
    public async Task<ActionResult<bool>> CloseAccountAsync(
        int accountId, 
        int clientId
        )
    {
        var closeAccount = await _accountService.CloseAccountAsync(accountId, clientId);

        return Ok(closeAccount);
    }

    [HttpPatch("{id:int}/plan")]
    public async Task<ActionResult<AccountResponseDto>> UpdatePlanAsync(
        int accountId, 
        int clientId, 
        AccountUpdateDto accountUpdateDto
        )
    {
        var updateAccountPlan = await _accountService.AccountUpdatePlanAsync(
            accountId, 
            clientId, 
            accountUpdateDto
            );

        if (updateAccountPlan is null)
        {
            return NotFound();
        }
        return Ok(updateAccountPlan);
    }
}