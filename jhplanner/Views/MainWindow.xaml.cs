using Microsoft.UI.Xaml;
using jhplanner.ViewModels;

namespace jhplanner.Views
{
    public sealed partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainWindowViewModel();
        }
    }
}