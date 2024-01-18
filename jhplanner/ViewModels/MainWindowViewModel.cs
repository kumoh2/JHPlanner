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

        public void OpenOrActivateToDoEditWindow(ToDoItemViewModel itemVM)
        {
            var existingWindow = Application.Current.Windows.OfType<ToDoEditWindow>().FirstOrDefault(w => w.ItemViewModel.DocumentNumber == itemVM.DocumentNumber);

            if (existingWindow != null)
            {
                existingWindow.Activate();
            }
            else
            {
                var editWindow = new ToDoEditWindow(itemVM);
                editWindow.Show();
            }
        }

        private string GenerateDocumentNumber()
        {
            var datePart = DateTime.Now.ToString("yyyyMMddHHmmssfff"); // 날짜와 시간을 밀리초까지 포함하여 포맷팅

            // 현재 날짜에 해당하는 모든 문서 번호를 가져와서 가장 큰 일련번호를 찾습니다.
            var maxNumberToday = _context.ToDoItems
                .AsEnumerable() // 데이터를 메모리로 가져온 후 처리
                .Where(t => t.DocumentNumber != null && t.DocumentNumber.StartsWith(datePart.Substring(0, 8)))
                .Select(t => {
                    var splitParts = t.DocumentNumber?.Split('-');
                    return splitParts?.Length > 1 && int.TryParse(splitParts[1], out int sequence) ? sequence : 0;
                })
                .DefaultIfEmpty(0) // 문서 번호가 없는 경우 기본값으로 0을 사용합니다.
                .Max();

            // 가장 큰 일련번호에 1을 더합니다.
            var sequenceNumber = maxNumberToday + 1;

            return $"{datePart}-{sequenceNumber}";
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
            // 문서번호가 없는 항목들만 먼저 가져옵니다.
            var itemsWithoutDocumentNumber = _context.ToDoItems
                .Where(item => string.IsNullOrEmpty(item.DocumentNumber))
                .ToList();

            // 문서번호가 없는 항목들에 대해 새로운 문서번호를 할당합니다.
            foreach (var item in itemsWithoutDocumentNumber)
            {
                item.DocumentNumber = GenerateDocumentNumber();
            }

            // 변경 사항을 데이터베이스에 저장합니다.
            _context.SaveChanges();

            var items = _context.ToDoItems.ToList();
            ToDoItems.Clear();
            foreach (var item in items)
            {
                ToDoItems.Add(new ToDoItemViewModel(item));
            }
            FilterToDoItems();
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