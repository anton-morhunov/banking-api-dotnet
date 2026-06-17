using BankAPI.Application.Interfaces.RepositoryInterfaces.Comments;
using BankAPI.Domain.Entities;
using BankAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Infrastructure.Repositories.Comments;

public class EfAccountCommentRepository : IAccountCommentRepository
{
    private readonly AppDbContext _db;
    public EfAccountCommentRepository(AppDbContext db)
    {
        _db = db;
    }
    public async Task<AccountComment> CreateAccountCommentAsync(AccountComment accountCommen)
    {
        await _db.AccountComments.AddAsync(accountCommen);
        await  _db.SaveChangesAsync();
        
        return accountCommen;
    }

    public async Task<AccountComment?> GetAccountCommentByIdAsync(int commentId)
    {
        return await _db.AccountComments
            .FirstOrDefaultAsync(x => x.Id == commentId);
    }

    public Task SaveChangesAsync()
    {
        return _db.SaveChangesAsync();
    }

    public async Task DeleteAccountCommentAsync(AccountComment accountComment)
    {
        var comment = await _db.AccountComments.FirstOrDefaultAsync(x => x.Id == accountComment.Id);

        if (comment is null)
        {
            return;
        }
        _db.AccountComments.Remove(comment);
        
        await _db.SaveChangesAsync();
    }

    public async Task<List<AccountComment>> GetCommentsByAccountIdAsync(int accountId)
    {
        return await  _db.AccountComments
            .AsNoTracking()
            .Where(x => x.AccountId == accountId)
            .OrderByDescending(x=>x.Id)
            .ToListAsync();
    }
}