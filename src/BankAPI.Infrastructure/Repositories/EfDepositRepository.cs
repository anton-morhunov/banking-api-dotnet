using BankAPI.Application.Interfaces.RepositoryInterfaces;
using BankAPI.Domain.Entities;
using BankAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Infrastructure.Repositories;

public class EfDepositRepository : IDepositRepository
{
    private readonly AppDbContext _context;
    
    public EfDepositRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Deposit> MakeDeposit(Deposit deposit)
    {
       _context.Deposits.Add(deposit);
       
       await _context.SaveChangesAsync();
       return deposit;
    }

    public async Task<List<Deposit>> GetAllDepositsByAccountId(int accountId)
    {
        return await _context.Deposits
            .AsNoTracking()
            .Where(x => x.AccountId == accountId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
        
    }

    public async Task<Deposit?> GetDepositById(Guid depositId)
    {
        return await _context.Deposits
            .FirstOrDefaultAsync(x => x.DepositId == depositId);
    }
}