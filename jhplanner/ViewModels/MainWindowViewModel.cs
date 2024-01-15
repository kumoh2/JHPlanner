using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using jhplanner.Data;
using jhplanner.Models;
using System.Windows;
using jhplanner.Views;

namespace jhplanner.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {

        private int _selectedFilterIndex;
        public int SelectedFilterIndex
        {
            get => _selectedFilterIndex;
            set
            {
                SetProperty(ref _selectedFilterIndex, value);
                FilterToDoItems();
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
                ToDoItems.Add(item);
            }
        }

        private readonly AppDbContext _context = new AppDbContext();
        public ObservableCollection<ToDoItem> ToDoItems { get; } = new ObservableCollection<ToDoItem>();

        // 새 ToDo 항목 추가 제거 커맨드
        public RelayCommand AddToDoCommand { get; }
        public RelayCommand<ToDoItem> RemoveToDoCommand { get; }

        public MainWindowViewModel()
        {
            LoadToDoItems();

            AddToDoCommand = new RelayCommand(AddToDoItem);
            RemoveToDoCommand = new RelayCommand<ToDoItem>(RemoveToDoItem);

        }

        private void RemoveToDoItem(ToDoItem? item)
        {
            if (item != null && MessageBox.Show("진짜 지우겠습니까?", "확인", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _context.ToDoItems.Remove(item);
                _context.SaveChanges();
                LoadToDoItems();
            }
        }

        private void LoadToDoItems()
        {
            var items = _context.ToDoItems.ToList();
            ToDoItems.Clear();
            foreach (var item in items)
            {
                ToDoItems.Add(item);
            }
        }

        private void AddToDoItem()
        {
            var newItem = new ToDoItem { Task = "새 항목", Details = "상세 설명", IsCompleted = false };
            var editWindow = new ToDoEditWindow(newItem);
            editWindow.Closed += (s, args) =>
            {
                if (editWindow.IsSaved)
                {
                    _context.ToDoItems.Add(newItem);
                    _context.SaveChanges();
                    LoadToDoItems(); // 데이터 갱신
                }
            };
            editWindow.Show(); // 모달리스 창으로 띄우기
        }

        // 여기에 필터링 및 항목 추가 로직을 추가할 수 있습니다.
    }
}
