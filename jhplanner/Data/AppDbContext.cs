using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using jhplanner.Models;

namespace jhplanner.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItem { get; set; }
        //public DbSet<TaskState> TaskState { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<TaskState>().HasData(
        //        new TaskState { StateId = 0, StateName = "진행중" },
        //        new TaskState { StateId = 1, StateName = "완료" }
        //    );
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "jhplanner.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}