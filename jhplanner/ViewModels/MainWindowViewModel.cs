using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using jhplanner.Data;
using jhplanner.Models;
using System.Windows;
using jhplanner.Views;

namespace jhplanner.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private int _selectedFilterIndex;
        private readonly AppDbContext _context = new AppDbContext();

        public int SelectedFilterIndex
        {
            get => _selectedFilterIndex;
            set
            {
                SetProperty(ref _selectedFilterIndex, value);
                FilterToDoItems();
            }
        }

        public ObservableCollection<ToDoItemViewModel> ToDoItems { get; } = new ObservableCollection<ToDoItemViewModel>();
        public RelayCommand AddToDoCommand { get; }
        public RelayCommand<ToDoItemViewModel?> RemoveToDoCommand { get; }

        public MainWindowViewModel()
        {
            LoadToDoItems();
            AddToDoCommand = new RelayCommand(AddToDoItem);
            RemoveToDoCommand = new RelayCommand<ToDoItemViewModel?>(RemoveToDoItem);
        }

        private void LoadToDoItems()
        {
            var items = _context.ToDoItems.ToList();
            ToDoItems.Clear();
            foreach (var item in items)
            {
                ToDoItems.Add(new ToDoItemViewModel(item));
            }
        }

        public void RefreshToDoItems()
        {
            LoadToDoItems();
        }

        private void AddToDoItem()
        {
            var newItemVM = new ToDoItemViewModel(new ToDoItem { Task = "새 항목", Details = "상세 설명", IsCompleted = false });
            var editWindow = new ToDoEditWindow(newItemVM, "New ToDo Item");
            editWindow.Closed += (s, args) =>
            {
                if (editWindow.IsSaved)
                {
                    _context.ToDoItems.Add(newItemVM.ToDoItem);
                    _context.SaveChanges();
                    LoadToDoItems();
                }
            };
            editWindow.Show();
        }

        private void RemoveToDoItem(ToDoItemViewModel? itemVM)
        {
            if (itemVM != null && MessageBox.Show("진짜 지우겠습니까?", "확인", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _context.ToDoItems.Remove(itemVM.ToDoItem);
                _context.SaveChanges();
                LoadToDoItems();
            }
        }

        private void FilterToDoItems()
        {
            var items = _context.ToDoItems.ToList();
            if (SelectedFilterIndex == 1) // 완료됨
            {
                items = items.Where(i => i.IsCompleted).ToList();
            }
            else if (SelectedFilterIndex == 2) // 미완료
            {
                items = items.Where(i => !i.IsCompleted).ToList();
            }

            ToDoItems.Clear();
            foreach (var item in items)
            {
                ToDoItems.Add(new ToDoItemViewModel(item));
            }
        }
    }
}