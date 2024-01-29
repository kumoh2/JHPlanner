using Microsoft.UI.Xaml;
using jhplanner.ViewModels;

namespace jhplanner.Views
{
    public sealed partial class DetailWindow : Window
    {
        public DetailWindowViewModel ViewModel { get; }
        public DetailWindow(int itemId)
        {
            ViewModel = new DetailWindowViewModel(itemId);
            ViewModel.CloseAction = () => this.Close();
            InitializeComponent();
        }
    }
}