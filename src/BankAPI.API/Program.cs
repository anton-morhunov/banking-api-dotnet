using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using BankAPI.Middleware;
using BankAPI.Application.Interfaces.RepositoryInterfaces;
using BankAPI.Infrastructure.Repositories;
using BankAPI.Application.Services;
using BankAPI.Application.Interfaces.ServiceInterfaces;
using BankAPI.Application.Validators.AccountValidators;
using BankAPI.Application.Validators.ClientValidators;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BankAPI.Application.Validators.CommentValidators;
using BankAPI.Application.Validators.DepositValidators;
using BankAPI.Application.Validators.Login_Validators;
using BankAPI.Application.Validators.UserValidators;
using BankAPI.Infrastructure.Data.Configurations;
using BankAPI.Domain.Entities;
using BankAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using BankAPI.Infrastructure.Services;
using FluentValidation.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .MinimumLevel.Information()
        .WriteTo.Console();
});
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter JWT token"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddScoped<IClientRepository, EfClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepository, EfAccountRepository>();
builder.Services.AddScoped<IUserRepository, EfUserRepository>();
builder.Services.AddScoped<IClientCommentRepository, EfClientCommentRepository>();
builder.Services.AddScoped<IAccountCommentRepository, EfAccountCommentRepository>();
builder.Services.AddScoped<IDepositRepository, EfDepositRepository>();
builder.Services.AddScoped<IAccountCommentService, AccountCommentService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher<UserModel>, PasswordHasher<UserModel>>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IGoogleAuthService, GoogleAuthService>();
builder.Services.AddScoped<IClientCommentService, ClientCommentService>();
builder.Services.AddScoped<IDepositService, DepositService>();
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ClientCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ClientsUpdateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AccountCreateValidators>();
builder.Services.AddValidatorsFromAssemblyContaining<AccountUpdateValidators>();
builder.Services.AddValidatorsFromAssemblyContaining<ClientCommentCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AccountCommentCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AccountCommentUpdateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserUpdateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<DepositValidator>();
var frontendUrl = builder.Configuration["Cors:FrontendUrl"];
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins(frontendUrl!)
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var jwtSettings = builder.Configuration
    .GetSection("Jwt")
    .Get<JwtSettings>();

if (!builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,

                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.SecretKey)
                ),

                ValidateIssuer = false,
                ValidateAudience = false,

                RoleClaimType = ClaimTypes.Role
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    return Task.CompletedTask;
                },

                OnTokenValidated = context =>
                {
                    return Task.CompletedTask;
                }
            };
        });
}

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt"));

builder.Services.Configure<AdminSettings>(
    builder.Configuration.GetSection("Admin"));

var app = builder.Build();

app.UseCors("AllowFrontend");
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

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