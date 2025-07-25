using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace jhplanner.Models
{
    public class ToDoItem : ObservableObject
    {
        private string _task;
        private string _details;
        private bool _stateId;

        public int Id { get; set; }

        public string Task
        {
            get => _task;
            set => SetProperty(ref _task, value);
        }

        public string Details
        {
            get => _details;
            set => SetProperty(ref _details, value);
        }

        public bool StateId
        {
            get => _stateId;
            set => SetProperty(ref _stateId, value);
        }
    }
}