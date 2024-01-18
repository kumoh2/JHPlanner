using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using jhplanner.ViewModels;

namespace jhplanner.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void ToDoList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is ToDoItemViewModel selectedItemVM)
            {
                string windowTitle = selectedItemVM.Task ?? "Edit ToDo Item";
                var editWindow = new ToDoEditWindow(selectedItemVM, windowTitle);
                editWindow.SaveCompleted += (s, args) =>
                {
                    var viewModel = DataContext as MainWindowViewModel;
                    viewModel?.RefreshToDoItems();
                };
                editWindow.Show();
            }
        }

    }
}