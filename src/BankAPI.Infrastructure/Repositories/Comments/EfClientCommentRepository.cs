using BankAPI.Application.Interfaces.RepositoryInterfaces.Comments;
using BankAPI.Domain.Entities;
using BankAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Infrastructure.Repositories.Comments;

public class EfClientCommentRepository : IClientCommentRepository
{
    private readonly AppDbContext _db;

    public EfClientCommentRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<ClientComment> CreateCommentAsync(ClientComment clientComment)
    {
        await _db.ClientComments.AddAsync(clientComment);
        await _db.SaveChangesAsync();

        return clientComment;
    }

    public async Task DeleteCommentAsync(ClientComment clientComment)
    {
         var comment = await _db.ClientComments.FirstOrDefaultAsync(x=>x.Id == clientComment.Id);

         if (comment == null)
             return;
         
         _db.ClientComments.Remove(comment);
         
        await _db.SaveChangesAsync();
    }

    public async Task<List<ClientComment>> GetCommentsByClientIdAsync(int id)
    {
        return await _db.ClientComments
            .AsNoTracking()
            .Where(x => x.ClientId == id)
            .OrderByDescending(x=>x.CreatedAt)
            .ToListAsync();
    }

    public async Task<ClientComment?> GetCommentByIdAsync(int id)
    {
        return await _db.ClientComments
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task SaveAsync()
    {
        return _db.SaveChangesAsync();
    }
}