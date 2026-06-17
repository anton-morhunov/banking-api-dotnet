using BankAPI.Domain.Entities;

namespace BankAPI.Application.Interfaces.ServiceInterfaces.Authentication;

public interface IJwtService
{
    string GenerateToken(UserModel userId);
}