using BankAPI.Domain.Entities;

namespace BankAPI.Application.Interfaces.ServiceInterfaces;

public interface IJwtService
{
    string GenerateToken(UserModel userId);
}