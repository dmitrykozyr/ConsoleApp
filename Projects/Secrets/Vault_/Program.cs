using Vault_.Extensions;

var builder = WebApplication.CreateBuilder(args);

IServiceCollection serviceCollection = builder.Services;

serviceCollection.AddHttpClient();
serviceCollection.AddControllers();
serviceCollection.AddAuthorization();
serviceCollection.AddAuthentication();
serviceCollection.AddEndpointsApiExplorer();
serviceCollection.AddControllersWithViews();

serviceCollection.AddOptionsExtensions();
serviceCollection.AddServicesExtensions();
IConfiguration configuration = serviceCollection.AddConfigurationExtension(builder);


WebApplication app = builder.Build();

serviceCollection.AddVaultExtensions(app, builder, configuration);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//!app.ApiGatewaySecretKeyCheck();

app.MapControllers();

await app.RunAsync();
