using BankAPI.Infrastructure.Data.Configurations;
using BankAPI.Domain.Enums;
using BankAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BankAPI.Infrastructure.Data;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(AppDbContext context, AdminSettings adminSettings)
    {
        if (context.Users.Any())
        {
            return;
        }

        var passwordHasher = new PasswordHasher<UserModel>();
        
        var user = new UserModel
        {
            Id = 1,
            Email = adminSettings.Email,
            Role = UserRole.Admin
        };

        user.PasswordHash = passwordHasher.HashPassword(user, adminSettings.Password);
        
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }
}