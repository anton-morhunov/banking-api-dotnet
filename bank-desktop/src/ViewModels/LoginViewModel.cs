using bank_desktop.src.Commands;
using System.Windows.Input;
using bank_desktop.Navigation;
using bank_desktop.src.Services.Interfaces;
using bank_desktop.src.DTOs.Requests;
using bank_desktop.src.DTOs.Responses;

namespace bank_desktop.src.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand LoginCommand { get; }
        private readonly IAuthService _authService;
        private readonly ITokenStorage _tokenStorage;
        private readonly INavigationService _navigationService;

        public LoginViewModel(
            IAuthService authService,
            ITokenStorage tokenStorage,
            INavigationService navigationService)
        {
            _authService = authService;
            _tokenStorage = tokenStorage;
            _navigationService = navigationService;

            LoginCommand = new AsyncRelayCommand(LoginAsync);
        }

        private string _email = "";

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        private async Task LoginAsync()
        {
            LoginRequestDto request = new()
            {
                Email = Email,
                Password = Password
            };

            LoginResponseDto response 
                = await _authService.LoginAsync(request);
            
            Console.WriteLine(response.Token);

            _tokenStorage.SetToken(response.Token);
            
            _navigationService.NavigateTo<ClientsViewModel>();
        }
    }
}
