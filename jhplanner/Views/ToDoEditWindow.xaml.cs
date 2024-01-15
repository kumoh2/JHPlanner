using System.Windows;
using System.Windows.Controls;
using jhplanner.ViewModels;

namespace jhplanner.Views
{
    public partial class ToDoEditWindow : Window
    {
        public bool IsSaved { get; private set; } = false; // 저장 여부를 나타내는 플래그
        public ToDoItemViewModel ItemViewModel { get; private set; }

        // 저장 완료 이벤트를 선언합니다.
        public event EventHandler? SaveCompleted;

        public ToDoEditWindow(ToDoItemViewModel itemViewModel)
        {
            InitializeComponent();
            ItemViewModel = itemViewModel;
            DataContext = ItemViewModel;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateBinding(TaskTextBox);
            UpdateBinding(DetailsTextBox);
            IsSaved = true;
            SaveCompleted?.Invoke(this, EventArgs.Empty); // 이벤트 발생
            this.Close();
        }

        private void UpdateBinding(TextBox textBox)
        {
            var bindingExpression = textBox.GetBindingExpression(TextBox.TextProperty);
            bindingExpression?.UpdateSource();
        }
    }
}
