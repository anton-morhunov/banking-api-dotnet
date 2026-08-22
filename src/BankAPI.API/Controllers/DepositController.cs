using BankAPI.Application.DTOs.DepositDto;
using BankAPI.Application.Interfaces.ServiceInterfaces.Deposits;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BankAPI.Controllers;

[ApiController]
[Route("api/deposit")]
public class DepositController : ControllerBase
{
    private readonly IDepositService _depositService;

    public DepositController(IDepositService depositService)
    {
        _depositService = depositService;
    }

    [EnableRateLimiting("fixed")]
    [Authorize(Roles = "Admin, Employee")]
    [HttpPost]
    public async Task<IActionResult> MakeDeposit(DepositCreateDto  depositCreateDto)
    {
        await _depositService.MakeDeposit(depositCreateDto);

        return Ok();
    }

    [EnableRateLimiting("fixed")]
    [Authorize(Roles = "Admin, Employee")]
    [HttpGet("deposit/{accountId:int}")]
    public async Task<ActionResult<IEnumerable<DepositResponseDto>>> GetAllDepositsByAccountId(int accountId)
    {
        var deposits = await _depositService.GetAllDepositsByAccountId(accountId);

        return Ok(deposits);
    }

    [EnableRateLimiting("fixed")]
    [Authorize(Roles = "Admin, Employee")]
    [HttpGet("{depositId:guid}")]
    public async Task<IActionResult> GetDepositById(Guid depositId)
    {
        var deposit = await _depositService.GetDepositById(depositId);
        return Ok(deposit);
    }
    
}