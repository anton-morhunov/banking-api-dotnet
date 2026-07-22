
namespace bank_desktop.src.Services.Interfaces
{
    public interface ITokenStorage
    {
        string? GetToken();

        void SetToken(string token);

        void ClearToken();
    }
}
