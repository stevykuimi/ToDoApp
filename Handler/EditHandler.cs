using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.Handlers
{
    public class EditHandler
    {
        private readonly ITodoService _service;

        public EditHandler(ITodoService service)
        {
            _service = service;
        }

        public async Task<TodoItem?> GetItemForEdit(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            return (item != null && !item.IsCompleted) ? item : null;
        }

        public async Task<IActionResult> HandleEdit(Guid id, TodoItem input)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null || existing.IsCompleted)
                return new BadRequestResult();

            // Avoid replacing the entity
            existing.Title = input.Title;
            existing.Description = input.Description;
            existing.DueDate = input.DueDate;
            existing.IsCompleted = input.IsCompleted;

            await _service.UpdateAsync(existing);
            return new RedirectToActionResult("Index", "Todo", null);
        }

    }
}
