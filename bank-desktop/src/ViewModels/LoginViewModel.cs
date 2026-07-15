using bank_desktop.src.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows;
using bank_desktop.src.Services.Interfaces;
using bank_desktop.src.DTOs.Requests;

namespace bank_desktop.src.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand LoginCommand { get; }
        public readonly IAuthService _authService;

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;

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
            var request = new LoginRequestDto
            {
                Email = Email,
                Password = Password
                
            };

            var response = await _authService.LoginAsync(request);

            MessageBox.Show(response.Token);
        }
    }
}
