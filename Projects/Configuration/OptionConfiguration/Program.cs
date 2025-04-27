using Microsoft.Extensions.Options;
using OptionConfiguration;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //  онфигурацию можно хранить в JSON и обращатьс€ к ней через IOptions
        // ≈сли данные в JSON изменились, дл€ их применени€ нужно перезапустить приложение
        builder.Services.ConfigureOptions<ApplicationOptionsSetup>();

        var app = builder.Build();

        app.UseHttpsRedirection();

        // ѕосле старта приложение€ при переходе по https://localhost:7138/options
        // получим значение из конфигурации
        app.MapGet("options", (IOptions<ApplicationOptions> options) =>
        {
            var response = new
            {
                options.Value.ExampleValue
            };

            return Results.Ok(response);
        });

        app.Run();
    }
}