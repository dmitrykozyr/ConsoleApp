using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Services;
using Domain.Services.API;
using Infrastructure.LoggingData;
using Infrastructure.Repositories;

/*
    ���� ��� ������� ��������� ����������:

    1. WebHost.CreateDefaultBuilder()

        � �������� ���-���������� �� ASP.NET Core
        � ������� � ����������� IWebHostBuilder, ���������� �� ��������� ���-������� (��������, Kestrel) � ������������ ��� ���-����������
        � ������������� ����������� ���������:
          � ������ ������������ �� appsettings.json, ���������� ��������� � ���������� ��������� ������
          � ��������� �����������
          � ��������� Kestrel � ������ ��������
        � ������������ ��� ���������� ASP.NET Core �� ������ 6

    2. Host.CreateDefaultBuilder()

        � �������� ����� ���������� .NET (�� ����������� ���)
        � ������� � ����������� IHostBuilder, ������� ����� ������������ ��� ���������� � ���-����������
        � ������������� ����������� ������������ � �����������, �� ����� �����������
        � �������� ��� ����������, ������� ����� �� �������� ���-������
        � ������������� ������������ ��� �������� ������� �����, ���������� � ASP.NET Core ����������, ������� � ������ 3.0

    3. WebApplication.CreateBuilder()

        � ������ � ASP.NET Core 6 ��� ���������� ������ �������� ���-����������
        � ������� WebApplicationBuilder, ������������� ����������� Host � WebHost � ����� �������
        � ��������� ��������� ������, �������� � middleware
        � ������������� �������� ������������ � �����������, ��������� ����� ��������� ���������� ���������� � ����� �����
        � ������������� ������������ ��� ����� �������� ASP.NET Core, ��� ��� ���� ������ ����� ����������� � �������� ������� ��������� ����������
        � ���������� ����� ��������������� API ��� ��������� ����������, ���������� ���������������� ����� ���������� �������
*/

//!
//apiVersion
//IOptions
// vault
// keycloak
// authorization
// authentication

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


// Services
builder.Services.AddScoped<IFileService, FileService>();

// Common services
builder.Services.AddScoped<ILogging, Logging>();
builder.Services.AddScoped<ILoginService, LoginService>();

// Repositories
builder.Services.AddScoped<IFileRepository, FileRepository>();


// IOptions


#region Middleware

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

#endregion
