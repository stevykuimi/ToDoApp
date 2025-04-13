using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Services;

namespace TodoApp.Handlers
{
    public class DeleteHandler
    {
        private readonly ITodoService _service;

        public DeleteHandler(ITodoService service)
        {
            _service = service;
        }

        public async Task<IActionResult> HandleDelete(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null || item.IsCompleted)
                return new BadRequestResult();

            await _service.DeleteAsync(id);
            return new RedirectToActionResult("Index", "Todo", null);
        }

        public async Task<IActionResult> GetDeleteView(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null || item.IsCompleted)
                return new BadRequestResult();

            return new ViewResult
            {
                ViewName = "Delete",
                ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<TodoApp.Models.TodoItem>(
                new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(), new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary())
                {
                    Model = item
                }
            };
        }
    }
}
