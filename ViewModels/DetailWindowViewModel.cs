using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using jhplanner;
using jhplanner.Data;
using jhplanner.Models;
using jhplanner.Views;

public class DetailWindowViewModel : ObservableObject
{
    private readonly AppDbContext _context;
    private ToDoItem _item;

    public ToDoItem Item
    {
        get => _item;
        set => SetProperty(ref _item, value);
    }

    public AsyncRelayCommand SaveCommand { get; }

    public Action CloseAction { get; set; }

    public DetailWindowViewModel(int itemId)
    {
        _context = new AppDbContext();
        SaveCommand = new AsyncRelayCommand(SaveItem);
        LoadItemAsync(itemId);
    }

    private async Task LoadItemAsync(int itemId)
    {
        Item = await _context.ToDoItem.FindAsync(itemId);
    }

    private async Task SaveItem()
    {
        if (Item != null)
        {
            _context.ToDoItem.Update(Item);
            await _context.SaveChangesAsync();

            // 기존 메인 윈도우를 닫고
            App.CloseMainWindow();

            // 새로운 메인 윈도우를 엽니다
            App.MainWindowInstance = new MainWindow();
            //App.MainWindowInstance.AppWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
            App.MainWindowInstance.Activate();
            CloseAction?.Invoke(); // 저장 후 창을 닫습니다.
        }
    }
}