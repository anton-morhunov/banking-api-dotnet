using BankAPI.Application.DTOs.Common;
using BankAPI.Application.Exceptions;

namespace BankAPI.Middleware;

public class ExceptionMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _environment;

    public ExceptionMiddleware(
        ILogger<ExceptionMiddleware> logger, 
        RequestDelegate next,
        IWebHostEnvironment environment)
    {
        _logger = logger;
        _next = next;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            if (ex is ApiExceptions apiExceptions)
            {
                _logger.LogWarning(ex, apiExceptions.Message);
                
                context.Response.StatusCode = (int)apiExceptions.StatusCode;

                await context.Response.WriteAsJsonAsync(new ErrorResponse
                {
                    StatusCode = (int)apiExceptions.StatusCode,
                    Message = apiExceptions.Message,
                    StackTrace = _environment.IsDevelopment() ? ex.StackTrace : null
                });
                
                return;
            }
            
            _logger.LogError(ex, "Unhandled exception");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsJsonAsync(new ErrorResponse
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = "Internal server error"
            });
        }
    }
}