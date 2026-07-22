using bank_desktop.src.Views;
using System.Windows;
using bank_desktop.src.ViewModels;

namespace bank_desktop
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}