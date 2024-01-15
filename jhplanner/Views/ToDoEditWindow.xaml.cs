using System.Windows;
using System.Windows.Controls;
using jhplanner.ViewModels;

namespace jhplanner.Views
{
    public partial class ToDoEditWindow : Window
    {
        public bool IsSaved { get; private set; } = false;
        public ToDoItemViewModel ItemViewModel { get; private set; }

        public event EventHandler? SaveCompleted;

        public ToDoEditWindow(ToDoItemViewModel itemViewModel, string windowTitle = "Edit ToDo Item")
        {
            InitializeComponent();
            ItemViewModel = itemViewModel;
            DataContext = ItemViewModel;
            this.Title = windowTitle; // 창 제목 설정
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateBinding(TaskTextBox);
            UpdateBinding(DetailsTextBox);
            IsSaved = true;
            SaveCompleted?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void UpdateBinding(TextBox textBox)
        {
            var bindingExpression = textBox.GetBindingExpression(TextBox.TextProperty);
            bindingExpression?.UpdateSource();
        }
    }
}
