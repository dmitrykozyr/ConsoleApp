using CommunityToolkit.Diagnostics;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.JsonDeserialize;
using Domain.Models.Options;
using Domain.Services;
using Domain.Services.API;
using Infrastructure.LoggingData;
using Infrastructure.Repositories;
using Infrastructure.Vault;
using Microsoft.Extensions.Options;

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
// keycloak
// authorization
// authentication

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// Services
builder.Services.AddScoped<IFileService, FileService>();

// Common services
builder.Services.AddScoped<ILogging, Logging>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped(typeof(IHttpClientData<>), typeof(HttpClientData<>));

// Repositories
builder.Services.AddScoped<IFileRepository, FileRepository>();

// IOptions
builder.Services.AddScoped<IOptions<VaultOptions>>();


#region ConfigurationBuilder

string environment = $"appsettings.{builder.Environment.EnvironmentName}.json";
builder.Configuration.AddJsonFile(environment, optional: false, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables(prefix: "VAULT_");
var cb = new ConfigurationBuilder();
cb.SetBasePath(Directory.GetCurrentDirectory());
cb.AddJsonFile(environment, optional: true, reloadOnChange: true);
cb.AddJsonFile("secrets.json", optional: true, reloadOnChange: true);
IConfiguration configuration = cb.Build();

//! ��� �����?
if (builder.Configuration.GetSection("VaultOptions")["Role"] is not null)
{
    var serviceProvider = builder.Services.BuildServiceProvider();

    var options = serviceProvider.GetRequiredService<IOptions<VaultOptions>>();
    var logging = serviceProvider.GetRequiredService<ILogging>();
    var httpClientData = serviceProvider.GetRequiredService<IHttpClientData<VaultSecrets>>();

    Guard.IsNotNull(options);
    Guard.IsNotNull(logging);
    Guard.IsNotNull(httpClientData);

    builder.Configuration.AddVault(
        //!
        //options =>
        //{
        //    IConfigurationSection? vaultOptions = builder.Configuration.GetSection("VaultOptions");

        //    options.Address     = vaultOptions["Address"];
        //    options.Role        = vaultOptions["Role"];
        //    options.MountPath   = vaultOptions["MountPath"];
        //    options.SecretType  = vaultOptions["SecretType"];
        //    options.Secret      = vaultOptions["Secret"];
        //},
        options,
        logging,
        configuration,
        httpClientData
    );
}

#endregion


#region Middleware

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); //! ��� ���?
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

#endregion
