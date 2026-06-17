using BankAPI.Application.Validators.AccountValidators;
using BankAPI.Application.Validators.ClientValidators;
using BankAPI.Application.Validators.CommentValidators;
using BankAPI.Application.Validators.DepositValidators;
using BankAPI.Application.Validators.Login_Validators;
using BankAPI.Application.Validators.TransferValidators;
using BankAPI.Application.Validators.UserValidators;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace BankAPI.DependencyInjection;

public static class ValidationRegistration
{
    public static IServiceCollection AddApplicationValidators(
        this IServiceCollection services
        )
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<ClientCreateValidator>();
        services.AddValidatorsFromAssemblyContaining<ClientsUpdateValidator>();
        services.AddValidatorsFromAssemblyContaining<AccountCreateValidators>();
        services.AddValidatorsFromAssemblyContaining<AccountUpdateValidators>();
        services.AddValidatorsFromAssemblyContaining<ClientCommentCreateValidator>();
        services.AddValidatorsFromAssemblyContaining<AccountCommentCreateValidator>();
        services.AddValidatorsFromAssemblyContaining<AccountCommentUpdateValidator>();
        services.AddValidatorsFromAssemblyContaining<LoginValidator>();
        services.AddValidatorsFromAssemblyContaining<UserCreateValidator>();
        services.AddValidatorsFromAssemblyContaining<UserUpdateValidator>();
        services.AddValidatorsFromAssemblyContaining<DepositValidator>();
        services.AddValidatorsFromAssemblyContaining<TransferValidator>();
        
        return services;
    }
}