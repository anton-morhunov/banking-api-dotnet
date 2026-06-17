using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankAPI.Infrastructure.DependencyInjection;

public static class CorsRegistration
{
    public static IServiceCollection AddCorsConfiguration(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var frontendUrl = configuration["Cors:FrontendUrl"];
        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend",
                policy => policy
                    .WithOrigins(frontendUrl!)
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });
        
        return services;
    }
}