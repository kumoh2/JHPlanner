using System.Collections.ObjectModel;
using System.Linq;
using jhplanner.Data;
using jhplanner.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Threading.Tasks;
using jhplanner.Views;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml.Controls;

namespace jhplanner.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly AppDbContext _context;
        private int _selectedFilterIndex;
        public ObservableCollection<ToDoItem> ToDoItems { get; private set; }
        public RelayCommand AddToDoCommand { get; }
        public RelayCommand<ToDoItem> RemoveToDoCommand { get; }
        public RelayCommand<ToDoItem> OpenDetailCommand { get; }

        public int SelectedFilterIndex
        {
            get => _selectedFilterIndex;
            set
            {
                SetProperty(ref _selectedFilterIndex, value);
                FilterToDoItems();
            }
        }

        public MainWindowViewModel()
        {
            _context = new AppDbContext();
            ToDoItems = new ObservableCollection<ToDoItem>(); // 초기화 추가
            SelectedFilterIndex = 0; // 콤보박스의 기본값을 "모두"로 설정
            LoadToDoItems();
            AddToDoCommand = new RelayCommand(AddToDoItem);
            RemoveToDoCommand = new RelayCommand<ToDoItem>(RemoveToDoItem);
            OpenDetailCommand = new RelayCommand<ToDoItem>(OpenDetail);
        }

        public void LoadToDoItems()
        {
            FilterToDoItems();
            foreach (var item in ToDoItems)
            {
                item.PropertyChanged += ToDoItem_PropertyChanged;
            }
            OnPropertyChanged(nameof(ToDoItems)); // 추가
        }

        private CancellationTokenSource debounceCts = new CancellationTokenSource();

        private async void ToDoItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is ToDoItem changedItem)
            {
                _context.Update(changedItem);
                await _context.SaveChangesAsync();
                if (e.PropertyName == nameof(ToDoItem.StateId))
                {
                    debounceCts.Cancel();  // 이전에 대기 중이던 필터링 로직을 취소합니다.
                    debounceCts = new CancellationTokenSource();  // 새로운 CancellationTokenSource를 생성합니다.

                    try
                    {
                        await Task.Delay(500, debounceCts.Token);  // 500ms 동안 대기합니다.
                        FilterToDoItems();  // 필터링 로직을 실행합니다.
                    }
                    catch (TaskCanceledException)
                    {
                        // 대기 중에 Task가 취소되면 아무것도 하지 않습니다.
                    }
                }
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
                // 사용자에게 삭제 확인 요청
                ContentDialog deleteDialog = new ContentDialog
                {
                    Title = "항목 삭제 확인",
                    Content = $"{item.Task} 작업을 삭제하시겠습니까?",
                    PrimaryButtonText = "삭제",
                    CloseButtonText = "취소"
                };
        
                ContentDialogResult result = await deleteDialog.ShowAsync();
        
                // 사용자가 '삭제'를 선택한 경우에만 항목 삭제
                if (result == ContentDialogResult.Primary)
                {
                    _context.ToDoItem.Remove(item);
                    await _context.SaveChangesAsync();
                    ToDoItems.Remove(item);
                }
            }
        }

        // DetailWindow 인스턴스를 관리하기 위한 Dictionary
        public static Dictionary<int, DetailWindow> DetailWindows = new Dictionary<int, DetailWindow>();

        public void OpenDetail(ToDoItem item)
        {
            if (item != null)
            {
                // 이미 열려 있는 DetailWindow가 있는지 확인
                if (DetailWindows.ContainsKey(item.Id))
                {
                    // 이미 열려 있는 DetailWindow를 활성화
                    DetailWindows[item.Id].Activate();
                }
                else
                {
                    // 새 DetailWindow를 생성하고 Dictionary에 추가
                    var detailWindow = new DetailWindow(item.Id);
                    DetailWindows.Add(item.Id, detailWindow);

                    // DetailWindow가 닫힐 때 Dictionary에서 제거하는 이벤트 핸들러 추가
                    detailWindow.Closed += (sender, e) =>
                    {
                        DetailWindows.Remove(item.Id);
                    };

                    // 활성화를 동기적으로 처리
                    detailWindow.DispatcherQueue.TryEnqueue(() => detailWindow.Activate());
                }
            }
        }

        private void FilterToDoItems()
        {
            // 데이터베이스에서 최신 데이터를 불러옴
            var items = _context.ToDoItem.ToList();

            // 필터링 조건에 따라 데이터를 필터링
            if (SelectedFilterIndex == 1) // 완료됨
            {
                items = items.Where(i => i.StateId).ToList();
            }
            else if (SelectedFilterIndex == 2) // 미완료
            {
                items = items.Where(i => !i.StateId).ToList();
            }
            // "모두" 선택 시 모든 항목 포함

            // 기존 컬렉션을 업데이트
            ToDoItems.Clear();
            foreach (var item in items)
            {
                ToDoItems.Add(item);
            }
        }
    }
}
