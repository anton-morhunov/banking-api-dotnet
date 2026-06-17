using BankAPI.Domain.Entities;

namespace BankAPI.Application.Interfaces.RepositoryInterfaces.Comments;

public interface IClientCommentRepository
{
    Task<ClientComment> CreateCommentAsync(ClientComment clientComment);
    Task DeleteCommentAsync(ClientComment clientComment);
    Task<List<ClientComment>> GetCommentsByClientIdAsync(int id);
    Task<ClientComment?> GetCommentByIdAsync(int id);
    Task SaveAsync();
}