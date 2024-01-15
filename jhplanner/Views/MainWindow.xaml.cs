using System.Text;
using System.Windows;
using jhplanner.Models;
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
        private void ToDoList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is ToDoItem selectedItem)
            {
                var editWindow = new ToDoEditWindow(selectedItem);
                editWindow.Show(); // 모달리스 창으로 띄우기
            }
        }

    }
}