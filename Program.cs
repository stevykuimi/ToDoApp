using Microsoft.EntityFrameworkCore;
using TodoApp.Handlers;
using TodoApp.Services;
using ToDoApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add EF Core DbContext with SQLite or in-memory fallback
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<CreateHandler>();
builder.Services.AddScoped<ViewDetailsHandler>();
builder.Services.AddScoped<EditHandler>();
builder.Services.AddScoped<DeleteHandler>();




var app = builder.Build();

// Auto-migrate DB on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Todo/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Todo}/{action=Index}/{id?}");

app.Run();
