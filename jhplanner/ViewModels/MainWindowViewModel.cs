using System.Collections.ObjectModel;
using System.Linq;
using jhplanner.Data;
using jhplanner.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Threading.Tasks;
using jhplanner.Views;

namespace jhplanner.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly AppDbContext _context;
        public ObservableCollection<ToDoItem> ToDoItems { get; private set; }
        public RelayCommand AddToDoCommand { get; }
        public RelayCommand<ToDoItem> RemoveToDoCommand { get; }
        public RelayCommand<ToDoItem> OpenDetailCommand { get; }

        public MainWindowViewModel()
        {
            _context = new AppDbContext();
            LoadToDoItems();
            AddToDoCommand = new RelayCommand(AddToDoItem);
            RemoveToDoCommand = new RelayCommand<ToDoItem>(RemoveToDoItem);
            OpenDetailCommand = new RelayCommand<ToDoItem>(OpenDetail);
        }

        private void LoadToDoItems()
        {
            var items = _context.ToDoItem.ToList();
            ToDoItems = new ObservableCollection<ToDoItem>(items);
            foreach (var item in ToDoItems)
            {
                item.PropertyChanged += ToDoItem_PropertyChanged;
            }
            OnPropertyChanged(nameof(ToDoItems)); // 추가
        }

        private async void ToDoItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is ToDoItem changedItem)
            {
                _context.Update(changedItem);
                await _context.SaveChangesAsync();
            }
        }
        public async void AddToDoItem()
        {
            var newItem = new ToDoItem { Task = "새 항목", Details = "", StateId = false };
            _context.ToDoItem.Add(newItem);
            await _context.SaveChangesAsync();
            ToDoItems.Add(newItem);
        }

        public async void RemoveToDoItem(ToDoItem item)
        {
            if (item != null)
            {
                _context.ToDoItem.Remove(item);
                await _context.SaveChangesAsync();
                ToDoItems.Remove(item);
            }
        }

        private void OpenDetail(ToDoItem item)
        {
            if (item != null)
            {
                //var detailWindow = new DetailWindow(item.Id);
                //detailWindow.Activate();
            }
        }
    }
}