using System.Windows;
using System.Windows.Controls;
using jhplanner.Models;

namespace jhplanner.Views
{
    public partial class ToDoEditWindow : Window
    {
        public bool IsSaved { get; private set; } = false; // 저장 여부를 나타내는 플래그
        public ToDoItem EditedItem { get; private set; }

        public ToDoEditWindow(ToDoItem item)
        {
            InitializeComponent();
            EditedItem = item;
            DataContext = item; // 아이템을 데이터 컨텍스트로 설정하여 데이터 바인딩을 통해 자동으로 표시되도록 함
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateBinding(TaskTextBox);
            UpdateBinding(DetailsTextBox);
            IsSaved = true; // 저장 플래그를 true로 설정
            this.Close(); // 창 닫기
        }

        private void UpdateBinding(TextBox textBox)
        {
            var bindingExpression = textBox.GetBindingExpression(TextBox.TextProperty);
            bindingExpression?.UpdateSource();
        }
    }
}