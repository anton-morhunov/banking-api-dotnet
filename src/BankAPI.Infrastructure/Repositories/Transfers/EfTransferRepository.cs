using BankAPI.Application.Interfaces.RepositoryInterfaces.Transfers;
using BankAPI.Domain.Entities;
using BankAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Infrastructure.Repositories.Transfers;

public class EfTransferRepository : ITransferRepository
{
    private readonly AppDbContext _context;
    
    public EfTransferRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Transfer> CreateTransferAsync(Transfer transfer)
    {
        _context.Transfers.Add(transfer);

         await _context.SaveChangesAsync();

         return transfer;
    }

    public async Task<List<Transfer>> GetAllTransfersByAccountIdAsync(int accountId)
    {
        return await _context.Transfers
            .AsNoTracking()
            .Where(x => x.SourceAccountId == accountId 
                        || x.DestinationAccountId == accountId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<Transfer?> GetTransferByIdAsync(Guid transferId)
    {
        return await _context.Transfers
            .FirstOrDefaultAsync(x => x.TransferId == transferId);
    }

    public async Task<List<Transfer>> GetOutgoingTransfersByAccountIdAsync(int accountId)
    {
        return await _context.Transfers
            .AsNoTracking()
            .Where(x => x.SourceAccountId == accountId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<Transfer>> GetIncomingTransfersByAccountIdAsync(int accountId)
    {
        return await _context.Transfers
            .AsNoTracking()
            .Where(x => x.DestinationAccountId == accountId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }
    
    public async Task<List<Transfer>> GetAllTransfersAsync()
    {
        return await _context.Transfers
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Transfer>> GetAllTransfersByUserIdAsync(int userId)
    {
        return await _context.Transfers
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }
    
}