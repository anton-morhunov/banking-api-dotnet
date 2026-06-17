using BankAPI.Application.Interfaces.ServiceInterfaces.Accounts;
using BankAPI.Application.Interfaces.ServiceInterfaces.Authentication;
using BankAPI.Application.Interfaces.ServiceInterfaces.Clients;
using BankAPI.Application.Interfaces.ServiceInterfaces.Comments;
using BankAPI.Application.Interfaces.ServiceInterfaces.Deposits;
using BankAPI.Application.Interfaces.ServiceInterfaces.Transfers;
using BankAPI.Application.Interfaces.ServiceInterfaces.Users;
using BankAPI.Application.Services.Accounts;
using BankAPI.Application.Services.Authentication;
using BankAPI.Application.Services.Clients;
using BankAPI.Application.Services.Comments;
using BankAPI.Application.Services.Deposits;
using BankAPI.Application.Services.Transfers;
using BankAPI.Application.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace BankAPI.Application.DependencyInjection;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services
        )
    {
        services.AddScoped<IAccountCommentService, AccountCommentService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IGoogleAuthService, GoogleAuthService>();
        services.AddScoped<IClientCommentService, ClientCommentService>();
        services.AddScoped<IDepositService, DepositService>();
        services.AddScoped<ITransferService, TransferService>();
        services.AddScoped<IClientService, ClientService>();
        
        return services;
    }
}