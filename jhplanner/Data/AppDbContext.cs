using Microsoft.EntityFrameworkCore;
using jhplanner.Models;

namespace jhplanner.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=jhplanner.db");
        }
    }
}