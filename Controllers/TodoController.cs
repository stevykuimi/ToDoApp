using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Services;
using TodoApp.Handlers;

public class TodoController : Controller
{
    private readonly ITodoService _todoService;
    private readonly EditHandler _editHandler;
    private readonly DeleteHandler _deleteHandler;
    private readonly CreateHandler _createHandler;
    private readonly ViewDetailsHandler _viewHandler;

    public TodoController(
        ITodoService todoService,
        EditHandler editHandler,
        DeleteHandler deleteHandler,
        CreateHandler createHandler,
        ViewDetailsHandler viewHandler)
    {
        _todoService = todoService;
        _editHandler = editHandler;
        _deleteHandler = deleteHandler;
        _createHandler = createHandler;
        _viewHandler = viewHandler;
    }

    public async Task<IActionResult> Index(string sortBy, bool? isCompleted)
        => View(await _todoService.GetAllAsync(sortBy, isCompleted));

    public async Task<IActionResult> Details(Guid id)
        => await _viewHandler.HandleDetails(id);

    public IActionResult Create()
        => View(_createHandler.PrepareNewItem());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TodoItem item)
        => await _createHandler.HandleCreate(item);

    public async Task<IActionResult> Edit(Guid id)
    {
        var item = await _editHandler.GetItemForEdit(id);
        return item == null ? BadRequest() : View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, TodoItem item)
        => await _editHandler.HandleEdit(id, item);

    public async Task<IActionResult> Delete(Guid id)
        => await _deleteHandler.GetDeleteView(id);

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
        => await _deleteHandler.HandleDelete(id);

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkCompleted(Guid id)
    {
        await _todoService.MarkCompletedAsync(id);
        return RedirectToAction(nameof(Index));
    }

}
