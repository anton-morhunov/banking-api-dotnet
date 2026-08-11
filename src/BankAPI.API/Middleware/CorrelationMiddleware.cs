namespace BankAPI.Middleware;
public class CorrelationMiddleware
{
    private const string HeaderName = "X-Correlation-Id";
    private readonly RequestDelegate _next;
    private  readonly ILogger<CorrelationMiddleware> _logger;
    
    public CorrelationMiddleware(
        RequestDelegate next,  
        ILogger<CorrelationMiddleware> logger
        )
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.Request.Headers.TryGetValue(HeaderName, out var header)
            ? header.ToString()
            : Guid.NewGuid().ToString();
        
        context.Items[HeaderName] = correlationId;
        
        context.Response.Headers[HeaderName] = correlationId;

        using (_logger.BeginScope(
                   new Dictionary<string, object>
                   {
                       ["CorrelationId"] = correlationId,
                       ["User"] = context.User.Identity?.Name ?? "Anonymous",
                       ["TraceId"] = context.TraceIdentifier,
                       ["IP"] =  context.Connection.RemoteIpAddress?.ToString()
                   }))
        {
            _logger.LogInformation("CorrelationId: {CorrelationId} assigned", correlationId);
        
            await _next(context);
        }
    }
}