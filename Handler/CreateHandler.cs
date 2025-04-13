using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.Handlers
{
    public class CreateHandler
    {
        private readonly ITodoService _service;

        public CreateHandler(ITodoService service)
        {
            _service = service;
        }

        public TodoItem PrepareNewItem()
        {
            return new TodoItem 
            { 
                Title = string.Empty,
                DueDate = DateTime.Today
            };
        }

        public async Task<IActionResult> HandleCreate(TodoItem item)
        {
            if (!IsValid(item))
                return new ViewResult { ViewName = "Create", ViewData = CreateViewData(item) };

            await _service.AddAsync(item);
            return new RedirectToActionResult("Index", "Todo", null);
        }

        private bool IsValid(TodoItem item)
        {
            return !string.IsNullOrWhiteSpace(item.Title) && item.DueDate != default;
        }

        private ViewDataDictionary<TodoItem> CreateViewData(TodoItem model)
        {
            return new ViewDataDictionary<TodoItem>(
                new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(),
                new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary())
            {
                Model = model
            };
        }
    }
}
