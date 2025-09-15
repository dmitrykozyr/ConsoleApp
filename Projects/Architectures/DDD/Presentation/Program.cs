using CommunityToolkit.Diagnostics;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.JsonDeserialize;
using Domain.Models.Options;
using Domain.Services;
using Domain.Services.API;
using Domain.Services.Login;
using Infrastructure.LoggingData;
using Infrastructure.Repositories;
using Infrastructure.Vault;
using Microsoft.Extensions.Options;
using Presentation;

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

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
builder.Services.AddApiVersioning();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

#region ConfigurationBuilder

string environment = $"appsettings.{builder.Environment.EnvironmentName}.json";
builder.Configuration.AddJsonFile(environment, optional: false, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables(prefix: "VAULT_");

var cb = new ConfigurationBuilder();
cb.SetBasePath(Directory.GetCurrentDirectory());
cb.AddJsonFile(environment, optional: true, reloadOnChange: true);
cb.AddJsonFile("secrets.json", optional: true, reloadOnChange: true);
IConfiguration configuration = cb.Build();

#endregion

// IOption
builder.Services.ConfigureOptions<ApplicationOptionsSetup<DatabaseOptions>>();
builder.Services.ConfigureOptions<ApplicationOptionsSetup<GeneralOptions>>();
builder.Services.ConfigureOptions<ApplicationOptionsSetup<LoginOptions>>();
builder.Services.ConfigureOptions<ApplicationOptionsSetup<VaultOptions>>();

// Services
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IDbConStrService, DbConStrService>();

// Common services
builder.Services.AddScoped<ILogging, Logging>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped(typeof(IHttpClientData<>), typeof(HttpClientData<>));

// Repositories
builder.Services.AddScoped<IFileRepository, FileRepository>();


#region Middleware

var app = builder.Build();

    #region HashiConf Vault

    using (var scope = app.Services.CreateScope())
    {
        var logging = scope.ServiceProvider.GetService<ILogging>();
        var httpClientData = scope.ServiceProvider.GetService<IHttpClientData<VaultSecrets>>();
        var vaultOptions = scope.ServiceProvider.GetService<IOptions<VaultOptions>>();

        Guard.IsNotNull(logging);
        Guard.IsNotNull(httpClientData);
        Guard.IsNotNull(vaultOptions);

        builder.Configuration.AddVault(vaultOptions, logging, configuration, httpClientData);
    }

    #endregion

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "FileDownload",
    pattern: "{controller=Home}/{action=FileDownload}/{id?}");

app.Run();

#endregion
