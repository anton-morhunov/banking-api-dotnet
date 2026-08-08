using BankAPI.Application.DTOs.Common;
using BankAPI.Application.Exceptions;

namespace BankAPI.Middleware;

public class ExceptionMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            if (ex is ApiExceptions apiExceptions)
            {
                context.Response.StatusCode = (int)apiExceptions.StatusCode;

                await context.Response.WriteAsJsonAsync(new ErrorResponse
                {
                    StatusCode = (int)apiExceptions.StatusCode,
                    Message = apiExceptions.Message,
                    Path = context.Request.Path,
                    TraceId = context.TraceIdentifier
                });
                
                return;
            }

            context.Response.StatusCode = 500;

            await context.Response.WriteAsJsonAsync(new ErrorResponse
            {
                StatusCode = 500,
                Message = "Internal server error"
            });
        }
    }
}