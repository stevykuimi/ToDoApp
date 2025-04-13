
using Microsoft.EntityFrameworkCore;
using TodoApp.Models;
using ToDoApp.Data;

namespace TodoApp.Services
{
    public class TodoService : ITodoService
    {
        private readonly AppDbContext _context;

        public TodoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TodoItem>> GetAllAsync(string? sortBy = null, bool? isCompleted = null)
        {
            IQueryable<TodoItem> query = _context.TodoItems;

            if (isCompleted.HasValue)
                query = query.Where(t => t.IsCompleted == isCompleted.Value);

            query = sortBy switch
            {
                "title" => query.OrderBy(t => t.Title),
                "due" => query.OrderBy(t => t.DueDate),
                _ => query.OrderBy(t => t.IsCompleted).ThenBy(t => t.DueDate)
            };

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(Guid id)
        {
            return await _context.TodoItems.FindAsync(id);
        }


        public async Task AddAsync(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(TodoItem item)
        {
            _context.TodoItems.Update(item);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(Guid id)
        {
            var item = await GetByIdAsync(id);
            if (item != null)
            {
                _context.TodoItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }


        public async Task MarkCompletedAsync(Guid id)
        {
            var item = await GetByIdAsync(id);
            if (item != null)
            {
                item.IsCompleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
