using bank_desktop.Navigation;

namespace bank_desktop.src.ViewModels;

public class MainViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;

    public MainViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        _navigationService.CurrentViewModelChanged += NavigationService_CurrentViewModelChanged;
        _navigationService.NavigateTo<LoginViewModel>();
    }

    public BaseViewModel CurrentViewModel
    {
        get => _navigationService.CurrentViewModel;
    }

    private void NavigationService_CurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}