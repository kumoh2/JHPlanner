using Microsoft.UI.Xaml;
using jhplanner.ViewModels;
using jhplanner.Models;
using Microsoft.UI.Xaml.Controls;

namespace jhplanner.Views
{
    public sealed partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; }
        public MainWindow()
        {
            ViewModel = new MainWindowViewModel();
            InitializeComponent();
        }

        private void DetailTextBlock_DoubleTapped(object sender, Microsoft.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            if (sender is TextBlock textBlock && textBlock.DataContext is ToDoItem item)
            {
                // 비동기 메서드 호출
                ViewModel.OpenDetail(item);
            }
        }
    }
}