using System.ComponentModel.DataAnnotations;

namespace jhplanner.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string? Task { get; set; }
        public string? Details { get; set; }
        public bool IsCompleted { get; set; }

        [Key]
        public string? DocumentNumber { get; set; } // 기본 키로 설정
    }
}
