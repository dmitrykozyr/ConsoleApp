using Microsoft.Extensions.Options;
using OptionConfiguration;

public class Program
{
    public static void Main()
    {
        var builder = WebApplication.CreateBuilder();

        //! ������������ ����� ������� � JSON � ���������� � ��� ����� IOptions
        // ���� ������ � JSON ����������, ��� �� ���������� ����� ������������� ����������
        builder.Services.ConfigureOptions<ApplicationOptionsSetup>();

        var app = builder.Build();

        app.UseHttpsRedirection();

        // ����� ������ ����������� ��� �������� �� https://localhost:7138/options
        // ������� �������� �� ������������
        app.MapGet("options", (IOptions<MyConfigs_1> options) =>
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