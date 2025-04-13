using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.Handlers
{
    public class ViewDetailsHandler
    {
        private readonly ITodoService _service;

        public ViewDetailsHandler(ITodoService service)
        {
            _service = service;
        }

        public async Task<IActionResult> HandleDetails(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null)
                return new NotFoundResult();

            return new ViewResult
            {
                ViewName = "Details",
                ViewData = new ViewDataDictionary<TodoItem>(
                    new EmptyModelMetadataProvider(),
                    new ModelStateDictionary())
                {
                    Model = item
                }
            };
        }
    }
}
