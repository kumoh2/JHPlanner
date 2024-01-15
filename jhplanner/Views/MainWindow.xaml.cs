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
                if (editWindow.ShowDialog() == true)
                {
                    // 데이터베이스 업데이트 및 리스트 갱신 로직
                }
            }
        }
    }
}