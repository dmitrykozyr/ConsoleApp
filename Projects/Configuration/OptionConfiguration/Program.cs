using Microsoft.Extensions.Options;
using OptionConfiguration;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // ������������ ����� ������� � JSON � ���������� � ��� ����� IOptions
        // ���� ������ � JSON ����������, ��� �� ���������� ����� ������������� ����������
        builder.Services.ConfigureOptions<ApplicationOptionsSetup>();

        var app = builder.Build();

        app.UseHttpsRedirection();

        // ����� ������ ����������� ��� �������� �� https://localhost:7138/options
        // ������� �������� �� ������������
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