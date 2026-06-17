using BankAPI.Application.Interfaces.RepositoryInterfaces.Accounts;
using BankAPI.Application.Interfaces.RepositoryInterfaces.Clients;
using BankAPI.Application.Interfaces.RepositoryInterfaces.Comments;
using BankAPI.Application.Interfaces.RepositoryInterfaces.Deposits;
using BankAPI.Application.Interfaces.RepositoryInterfaces.Transfers;
using BankAPI.Application.Interfaces.RepositoryInterfaces.Users;
using BankAPI.Application.Interfaces.ServiceInterfaces.Authentication;
using BankAPI.Domain.Entities;
using BankAPI.Infrastructure.Repositories.Accounts;
using BankAPI.Infrastructure.Repositories.Clients;
using BankAPI.Infrastructure.Repositories.Comments;
using BankAPI.Infrastructure.Repositories.Deposits;
using BankAPI.Infrastructure.Repositories.Transfers;
using BankAPI.Infrastructure.Repositories.Users;
using BankAPI.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BankAPI.Infrastructure.DependencyInjection;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services
        )
    {
        services.AddScoped<IClientRepository, EfClientRepository>();
        services.AddScoped<IAccountRepository, EfAccountRepository>();
        services.AddScoped<IUserRepository, EfUserRepository>();
        services.AddScoped<IClientCommentRepository, EfClientCommentRepository>();
        services.AddScoped<IAccountCommentRepository, EfAccountCommentRepository>();
        services.AddScoped<IDepositRepository, EfDepositRepository>();
        services.AddScoped<ITransferRepository,  EfTransferRepository>();
        
        services.AddScoped<IPasswordHasher<UserModel>, PasswordHasher<UserModel>>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IJwtService, JwtService>();
        
        return services;
    }
}