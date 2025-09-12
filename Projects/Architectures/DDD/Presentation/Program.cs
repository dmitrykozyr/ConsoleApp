using Domain.Interfaces;
using Infrastructure.LoggingData;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


// Services


// Common services
builder.Services.AddScoped<ILogging, Logging>();


// Repositories



// IOptions




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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
