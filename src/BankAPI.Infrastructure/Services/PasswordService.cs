using BankAPI.Application.Interfaces.ServiceInterfaces;
using BankAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BankAPI.Infrastructure.Services;

public class PasswordService : IPasswordService
{
    private readonly IPasswordHasher<UserModel> _hasher;

    public PasswordService(IPasswordHasher<UserModel> hasher)
    {
        _hasher = hasher;
    }

    public string Hash(string password)
    {
        // user can be null here — it's fine for hashing
        return _hasher.HashPassword(null, password);
    }

    public bool Verify(string password, string hash)
    {
        var result = _hasher.VerifyHashedPassword(null, hash, password);
        return result != PasswordVerificationResult.Failed;
    }
}