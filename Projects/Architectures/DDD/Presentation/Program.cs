using CommunityToolkit.Diagnostics;
using Domain.Interfaces;
using Domain.Models.JsonDeserialize;
using Domain.Models.Options;
using Infrastructure.Vault;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using Presentation.Extensions;

// ��� ������� ��������� ����������
/*
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
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient(); //! ��������� ��� ������������� IHttpClientFactory
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddFeatureManagement(builder.Configuration.GetSection("FeatureFlags"));

builder.Services.AddRedisExtensions(builder);
builder.Services.AddOptionsExtensions(builder);
builder.Services.AddServicesExtensions(builder);
builder.Services.AddRepositoriesExtensions(builder);
IConfiguration configuration = builder.Services.AddConfigurationExtension(builder);


#region Middleware

WebApplication app = builder.Build();

//! ������� � ����� ����������
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
