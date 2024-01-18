namespace jhplanner.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string? Task { get; set; }
        public string? Details { get; set; }
        public bool IsCompleted { get; set; }
    }
}
