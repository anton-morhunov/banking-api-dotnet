namespace BankAPI.Application.Interfaces.ServiceInterfaces;

public interface IPasswordService
{
    string Hash(string password);
    bool Verify(string password, string hash);
}