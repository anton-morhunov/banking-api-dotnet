using BankAPI.Application.DTOs.TransferDto;
using BankAPI.Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers;

[ApiController]
[Route("api/transfers")]
public class TransferController : ControllerBase
{
    private readonly ITransferService _transferService;
    
    public TransferController(ITransferService transferService)
    {
        _transferService = transferService;
    }

    [Authorize(Roles = "Admin, Employee")]
    [HttpPost]
    public async Task<ActionResult<TransferResponseDto>> CreateTransferAsync(CreateTransferDto createTransferDto)
    {
        var response = await _transferService.CreateTransferAsync(createTransferDto);
        return Ok(response);
    }

    [Authorize(Roles = "Admin, Employee")]
    [HttpGet("accounts/{accountId:int}")]
    public async Task<ActionResult<IEnumerable<TransferResponseDto>>> GetAllTransfersByAccountIdAsync(int accountId)
    {
        var response = await _transferService.GetAllTransfersByAccountIdAsync(accountId);
        return Ok(response);
    }

    [Authorize(Roles = "Admin, Employee")]
    [HttpGet("{transferId:guid}")]
    public async Task<ActionResult<TransferResponseDto>> GetTransferByIdAsync(Guid transferId)
    {
        var response = await _transferService.GetTransferByIdAsync(transferId);
        
        return Ok(response);
    }

    [Authorize(Roles = "Admin, Employee")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransferResponseDto>>> GetAllTransfersAsync()
    {
        var response = await _transferService.GetAllTransfersAsync();
        return Ok(response);
    }

    [Authorize(Roles = "Admin, Employee")]
    [HttpGet("accounts/{accountId:int}/outgoing")]
    public async Task<ActionResult<IEnumerable<TransferResponseDto>>> GetOutgoingTransfersByAccountIdAsync(
        int accountId)
    {
        var response = await _transferService.GetOutgoingTransfersByAccountIdAsync(accountId);
        return Ok(response);
    }

    [Authorize(Roles = "Admin, Employee")]
    [HttpGet("accounts/{accountId:int}/incoming")]
    public async Task<ActionResult<IEnumerable<TransferResponseDto>>> GetIncomingTransfersByAccountIdAsync(
        int accountId)
    {
        var response = await _transferService.GetIncomingTransfersByAccountIdAsync(accountId);
        return Ok(response);
    }

    [Authorize(Roles = "Admin, Employee")]
    [HttpGet("users/{userId}")]
    public async Task<ActionResult<IEnumerable<TransferResponseDto>>> GetAllTransfersByUserIdAsync(int userId)
    {
        var response = await _transferService.GetAllTransfersByUserIdAsync(userId);
        return Ok(response);
    }
}