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
                var task = _item.Task; // 임시 변수 할당
                SetProperty(ref task, value); // 임시 변수를 사용하여 SetProperty 호출
                _item.Task = task; // ToDoItem의 속성 업데이트
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