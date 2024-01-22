using Microsoft.EntityFrameworkCore;
using ToDoAngularGraphQL.Server.DTO;
using ToDoAngularGraphQL.Server.Models;
using ToDoAngularGraphQL.Server.Services;

namespace ToDoAngularGraphQL.Server.Schema
{
    public class Mutation
    {
        public async Task<ToDoItem> AddToDo(ToDoInput input, ToDoContext _context)
        {
            var newToDo = new ToDoItem
            {
                Item = input.Item,
                Title = input.Title,
                IsDone = false
            };
            await _context.AddAsync(newToDo);
            await _context.SaveChangesAsync();
            return newToDo;
        }

        public async Task<bool> DeleteToDo(int id, ToDoContext _context)
        {
            var toBeDeleted = _context.ToDoItems.Find(id);
            if (toBeDeleted != null)
            {
                _context.Remove(toBeDeleted);
                await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<bool> DeleteAll(ToDoContext _context)
        {
            if (_context.ToDoItems.Count() > 0)
            {
                _context.RemoveRange(_context.ToDoItems);
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT (ToDoItems, RESEED, 0)");
                return true;
            }
            else
                return false;
        }

        public async Task<ToDoItem?> checkToDo(int id, ToDoContext _context)
        {
            var todo = _context.ToDoItems.FirstOrDefault(t => t.Id == id);
            if (todo != null)
            {
                todo.IsDone = !todo.IsDone;
                _context.Update(todo);
                await _context.SaveChangesAsync();
                return todo;
            }
            else
                return null;
        }
    }
}
