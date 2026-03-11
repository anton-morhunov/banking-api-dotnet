using BankAPI.Repositories.Interfaces;
using BankAPI.Data;
using BankAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Repositories;

public class EfUserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public EfUserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserModel?> GetUserByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<UserModel?> GetUserByIdAsync(int id)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task SaveUserAsync()
    {
        return _context.SaveChangesAsync();
    }

    public async Task<UserModel> CreateUserAsync(UserModel user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }
}