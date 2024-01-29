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
            // 이벤트가 발생한 TextBlock에서 DataContext를 가져옵니다.
            var textBlock = sender as TextBlock;
            var item = textBlock.DataContext as ToDoItem;

            // 해당 ToDoItem에 대한 DetailWindow를 연다.
            if (item != null)
            {
                // 비동기 메서드 호출
                ViewModel.OpenDetail(item);
            }
        }
    }
}