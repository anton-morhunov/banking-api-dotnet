using BankAPI.Infrastructure.Data.Configurations;
using BankAPI.Domain.Enums;
using BankAPI.Domain.Entities;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BankAPI.Infrastructure.Data;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(AppDbContext context, AdminSettings adminSettings, IHostEnvironment env)
    {
        if (!context.Users.Any())
        {
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
        
        if (!env.IsDevelopment())
        {
            return;
        }
        
        if (await context.Clients.AnyAsync() ||
            await context.Accounts.AnyAsync()
            )
        {
            return;
        }
        
        var clientFaker = new Faker<ClientModel>()
            .RuleFor(x => x.Name, f => f.Name.FullName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.PhoneNumber,
                f => $"+{f.Random.Long(1000000000, 999999999999999)}");

        var clients = clientFaker.Generate(500);

        await context.Clients.AddRangeAsync(clients);
        await context.SaveChangesAsync();

        var random = new Random();

        var accounts = new List<AccountModel>();

        foreach (var client in clients)
        {
            accounts.Add(new AccountModel
            {
                ClientId = client.Id,
                AccountType = (AccountType)random.Next(0, 2),
                Status = 0,
                Balance = random.Next(1000, 50000)
            });
        }

        await context.Accounts.AddRangeAsync(accounts);
        await context.SaveChangesAsync();
    }
}