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

//!serviceCollection.AddVaultExtensions(app, builder, configuration);

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

// Проверяем наличие секретного ключа
app.Use(async (context, next) =>
{
    const string OCELOT_SECRET_KEY = "ocelotSecretKey";
    const string YARP_SECRET_KEY = "yarpSecretKey";

    bool isOcelotSecretExists = context.Request.Headers.TryGetValue("OcelotGatewaySecretKey", out var ocelotSecret);
    bool isYarpSecretExists = context.Request.Headers.TryGetValue("YarpGatewaySecretKey", out var yarpScret);

    bool isRequestGrantedByOcelot = isOcelotSecretExists && ocelotSecret == OCELOT_SECRET_KEY;
    bool isRequestGrantedByYarp = isYarpSecretExists && yarpScret == YARP_SECRET_KEY;

    if (isRequestGrantedByOcelot || isRequestGrantedByYarp)
    {
        await next();
    }
    else
    {
        context.Response.StatusCode = 403;

        await context.Response.WriteAsync("Прямой доступ запрещен. Используйте API Gateway.");
    }
});

app.MapControllers();

await app.RunAsync();
