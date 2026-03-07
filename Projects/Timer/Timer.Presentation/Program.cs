using Timer.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

IServiceCollection serviceCollection = builder.Services;

serviceCollection.AddRazorPages();

serviceCollection.AddRepositoriesExtensions();





WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
