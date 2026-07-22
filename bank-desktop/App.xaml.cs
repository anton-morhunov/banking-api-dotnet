using bank_desktop.src.Services.Implementations;
using bank_desktop.src.Services.Interfaces;
using bank_desktop.src.ViewModels;
using bank_desktop.src.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using bank_desktop.Navigation;
using Microsoft.Extensions.Configuration;
using bank_desktop.src.Handlers;

namespace bank_desktop
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();

            IConfiguration configuration =
                 BuildConfiguration();

            services.AddSingleton<IConfiguration>(configuration);

            ConfigureServices(services);

            _serviceProvider =
                services.BuildServiceProvider();
        }

        private IConfiguration BuildConfiguration()
        {
            return new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", 
                optional: false, 
                reloadOnChange: true)
                .Build();
        }
        

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = 
                _serviceProvider.GetService<MainWindow>();

            mainWindow.Show();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<LoginView>();
            services.AddSingleton<IAuthService, AuthService>();
            services.AddHttpClient<IAuthService, AuthService>((provider, client) =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();

                client.BaseAddress = new Uri(configuration["ApiSettings:BaseUrl"]!);
            }).AddHttpMessageHandler<JwtHandler>();
            services.AddSingleton<ITokenStorage, TokenStorage>();
            services.AddTransient<JwtHandler>();
            services.AddHttpClient<IClientService, ClientService>((provider, client) =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                
                client.BaseAddress = new Uri(configuration["ApiSettings:BaseUrl"]!);
            }).AddHttpMessageHandler<JwtHandler>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<MainViewModel>();
            services.AddTransient<ClientsViewModel>();
        }
    }

}
