using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using jhplanner.Data;
using jhplanner.Models;
using System;
using System.Threading.Tasks;

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
        OnPropertyChanged(nameof(Item)); // 로드된 Item에 대한 변경을 UI에 알립니다.
    }

    private async Task SaveItem()
    {
        if (Item != null)
        {
            _context.ToDoItem.Update(Item);
            await _context.SaveChangesAsync();
            CloseAction?.Invoke(); // 저장 후 창을 닫습니다.
        }
    }
}