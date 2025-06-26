using Microsoft.EntityFrameworkCore;
using todolist_api.Model;

namespace todolist_api.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}