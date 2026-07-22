using bank_desktop.src.Services.Interfaces;

namespace bank_desktop.src.Services.Implementations
{
    public class TokenStorage : ITokenStorage
    {
        private string? _token;
        public string? GetToken()
        { 
            return _token;
        }

        public void SetToken(string token)
        {
            _token = token;
        }

        public void ClearToken()
        {
            _token = null;
   
        }
    }
}
