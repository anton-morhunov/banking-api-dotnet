using Microsoft.EntityFrameworkCore;
using BankAPI.Domain.Entities;

namespace BankAPI.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<ClientModel> Clients { get; set; }
    public DbSet<AccountModel> Accounts { get; set; }
    public DbSet<UserModel> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : 
        base(options){}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}