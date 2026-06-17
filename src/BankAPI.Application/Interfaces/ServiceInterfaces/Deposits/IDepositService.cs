using BankAPI.Application.DTOs.DepositDto;

namespace BankAPI.Application.Interfaces.ServiceInterfaces.Deposits;

public interface IDepositService
{
    Task<DepositResponseDto> MakeDeposit(DepositCreateDto depositCreateDto);
    Task<List<DepositResponseDto>> GetAllDepositsByAccountId(int accountId);
    Task<DepositResponseDto> GetDepositById(Guid depositId);
}