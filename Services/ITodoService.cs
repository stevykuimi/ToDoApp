using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Services
{
    public interface ITodoService
    {
        Task<List<TodoItem>> GetAllAsync(string? sortBy = null, bool? isCompleted = null);
        Task<TodoItem?> GetByIdAsync(Guid id);
        Task AddAsync(TodoItem item);
        Task UpdateAsync(TodoItem item);
        Task DeleteAsync(Guid id);
        Task MarkCompletedAsync(Guid id);
    }
}
