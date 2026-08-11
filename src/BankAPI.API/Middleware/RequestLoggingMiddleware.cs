using System.Diagnostics;

namespace BankAPI.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(
        RequestDelegate next, 
        ILogger<RequestLoggingMiddleware> logger
        )
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation(
            "→ {Method} {Path}", 
            context.Request.Method, 
            context.Request.Path);
        
        var stopwatch = Stopwatch.StartNew();
        var userAgent = context.Request.Headers.UserAgent.ToString();
        
        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();
        
            _logger.LogInformation(
                "← {Method} {Path} {StatusCode} ({ElapsedMilliseconds} ms)",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds);
        }
        
    }
}