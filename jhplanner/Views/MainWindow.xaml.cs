using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using jhplanner.ViewModels;

namespace jhplanner.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        // MainWindow.xaml.cs
        private void ToDoList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is ToDoItemViewModel selectedItemVM)
            {
                var editWindow = new ToDoEditWindow(selectedItemVM);
                editWindow.SaveCompleted += (s, args) =>
                {
                    // ViewModel의 LoadToDoItems 메소드를 호출하여 리스트를 갱신합니다.
                    var viewModel = DataContext as MainWindowViewModel;
                    viewModel?.RefreshToDoItems();
                };
                editWindow.Show();
            }
        }

    }
}