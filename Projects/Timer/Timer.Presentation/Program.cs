using Timer.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

IServiceCollection serviceCollection = builder.Services;

serviceCollection.AddRazorPages();

serviceCollection.AddHttpClient();
serviceCollection.AddControllers();
serviceCollection.AddAuthorization();
serviceCollection.AddAuthentication();
serviceCollection.AddEndpointsApiExplorer();
serviceCollection.AddControllersWithViews();

serviceCollection.AddOptionsExtensions();
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

app.MapControllers();
app.MapRazorPages();

app.Run();
