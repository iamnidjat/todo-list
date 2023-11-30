using Microsoft.EntityFrameworkCore;
using ToDoListWebApi.Models;

namespace ToDoListWebApi.Services
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<TaskEntity> Tasks { get; set; }
    }
}
