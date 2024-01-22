using Microsoft.EntityFrameworkCore;
using ToDoAngularGraphQL.Server.Models;
using ToDoAngularGraphQL.Server.Services;

namespace ToDoAngularGraphQL.Server.Schema
{
    public class Query
    {
        public List<ToDoItem> GetTodos(ToDoContext context)
        {
            return context.ToDoItems.AsNoTracking().ToList();
        }
    }
}
