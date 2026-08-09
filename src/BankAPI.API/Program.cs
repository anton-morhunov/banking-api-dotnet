using BankAPI.Middleware;
using Serilog;
using BankAPI.Application.DependencyInjection;
using BankAPI.DependencyInjection;
using BankAPI.Infrastructure.Data.Configurations;
using BankAPI.Infrastructure.DependencyInjection;
using BankAPI.Infrastructure.Initialization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .MinimumLevel.Information()
        .WriteTo.Console();
});

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationValidators();
builder.Services.AddControllers();
builder.Services.AddCorsConfiguration(builder.Configuration);

if (!builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddJwtAuthentication(builder.Configuration);
}

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt"));

builder.Services.Configure<AdminSettings>(
    builder.Configuration.GetSection("Admin"));

var app = builder.Build();

await app.InitializeDatabaseAsync();

app.UseCors("AllowFrontend");
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

var port = Environment.GetEnvironmentVariable("PORT");
if (port != null)
{
    app.Urls.Add($"http://*:{port}");
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

public partial class Program { }