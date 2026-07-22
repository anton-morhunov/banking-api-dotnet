using bank_desktop.src.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace bank_desktop.Navigation;

public class NavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;
    private BaseViewModel _currentViewModel;
    public event Action? CurrentViewModelChanged;

    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public BaseViewModel CurrentViewModel
    {
        get => _currentViewModel;

        private set { _currentViewModel = value; }
    }

    public void NavigateTo<TViewModel>()
        where TViewModel : BaseViewModel
    {
        var viewModel = _serviceProvider.GetRequiredService<TViewModel>();
        CurrentViewModel = viewModel;
        CurrentViewModelChanged?.Invoke();
    }

}