using System.Collections.ObjectModel;
using bank_desktop.DTOs.Clients;
using bank_desktop.src.Services.Interfaces;

namespace bank_desktop.src.ViewModels;

public class ClientsViewModel : BaseViewModel
{
    private readonly IClientService _clientService;
    public ObservableCollection<ClientResponseDto> Clients { get; set; }

    public ClientsViewModel(IClientService clientService)
    {
        _clientService = clientService;
        
        _ = LoadClientsAsync();
    }

    private async Task LoadClientsAsync()
    {
        var clients = await _clientService.GetAllClientsAsync();
        
        Clients = new ObservableCollection<ClientResponseDto>(clients);
        OnPropertyChanged(nameof(Clients));
    }
}