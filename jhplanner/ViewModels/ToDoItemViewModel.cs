using CommunityToolkit.Mvvm.ComponentModel;
using jhplanner.Models;

namespace jhplanner.ViewModels
{
    public class ToDoItemViewModel : ObservableObject
    {
        private ToDoItem _item;

        public ToDoItemViewModel(ToDoItem item)
        {
            _item = item;
        }

        public string? Task
        {
            get => _item.Task;
            set
            {
                var task = _item.Task;
                SetProperty(ref task, value);
                _item.Task = task;
            }
        }

        public string? Details
        {
            get => _item.Details;
            set
            {
                var details = _item.Details;
                SetProperty(ref details, value);
                _item.Details = details;
            }
        }

        public bool IsCompleted
        {
            get => _item.IsCompleted;
            set
            {
                var isCompleted = _item.IsCompleted;
                SetProperty(ref isCompleted, value);
                _item.IsCompleted = isCompleted;
            }
        }

        public ToDoItem ToDoItem
        {
            get => _item;
        }
    }
}
