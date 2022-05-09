using Microsoft.EntityFrameworkCore;
using MVC.Data;

// Подключение сервисов
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

// DefaultConnection - это имя ConnectionString в appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

// Middleware
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
