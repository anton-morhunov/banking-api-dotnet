using System.Threading.RateLimiting;

namespace BankAPI.Extensions;

public static class RateLimitingExtensions
{
    public static IServiceCollection AddApiRateLimiter(
        this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.OnRejected = async (context, cancellationToken) =>
            {
                context.HttpContext.Response.StatusCode =
                    StatusCodes.Status429TooManyRequests;
                context.HttpContext.Response.Headers.RetryAfter = "10";

                await context.HttpContext.Response.WriteAsJsonAsync(
                    new
                    {
                        StatusCode = 429,
                        Message = "Too many requests."
                    }, cancellationToken);
            };
            options.AddPolicy("fixed", context =>
            {
                var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

                return RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: ip,
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 5,
                        Window = TimeSpan.FromSeconds(10),
                        QueueLimit = 0
                    });
            });
            
            options.AddPolicy("user-limit", context =>
            {
                var userId = context.User.FindFirst("sub")?.Value;

                return RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: userId,
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 20,
                        Window = TimeSpan.FromSeconds(10),
                        QueueLimit = 0
                    });
            });
        });
        
        return services;
    }
}