using System.Windows;
using jhplanner.Models;

namespace jhplanner.Views
{
    public partial class ToDoEditWindow : Window
    {
        public ToDoItem EditedItem { get; private set; }

        public ToDoEditWindow(ToDoItem item)
        {
            InitializeComponent();
            EditedItem = item;
            DataContext = item; // 아이템을 데이터 컨텍스트로 설정하여 데이터 바인딩을 통해 자동으로 표시되도록 함
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}