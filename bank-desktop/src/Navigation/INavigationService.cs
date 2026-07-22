using bank_desktop.src.ViewModels;

namespace bank_desktop.Navigation;

public interface INavigationService
{
    BaseViewModel CurrentViewModel { get; }
    void NavigateTo<TViewModel>() 
        where TViewModel : BaseViewModel;
    event Action? CurrentViewModelChanged;
}