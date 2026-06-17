using BankAPI.Infrastructure.Data;
using BankAPI.Infrastructure.Data.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BankAPI.Infrastructure.Initialization;

public static class DatabaseInitializer
{
    public static async Task InitializeDatabaseAsync(
        this WebApplication app
    )
    {
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var adminSetting = scope.ServiceProvider
                .GetRequiredService<IOptions<AdminSettings>>().Value;
    
            if (db.Database.IsRelational())
            {
                db.Database.Migrate();
                await DatabaseSeeder.SeedAsync(
                    db, 
                    adminSetting,
                    app.Environment
                );
            }
        }
    }
}