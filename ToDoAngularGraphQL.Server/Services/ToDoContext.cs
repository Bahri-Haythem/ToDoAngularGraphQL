using Microsoft.EntityFrameworkCore;
using ToDoAngularGraphQL.Server.Models;

namespace ToDoAngularGraphQL.Server.Services
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
        }
        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
