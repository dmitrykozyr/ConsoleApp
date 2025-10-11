using Microsoft.FeatureManagement;
using Presentation.Extensions;

// WebHost.CreateDefaultBuilder()
/*
    �������� ���-���������� �� ASP.NET Core
    ������� � ����������� IWebHostBuilder, ���������� �� ��������� ���-������� (��������, Kestrel) � ������������ ��� ���-����������
    ������������� ����������� ���������:
    � ������ ������������ �� appsettings.json, ���������� ��������� � ���������� ��������� ������
    � ��������� �����������
    � ��������� Kestrel � ������ ��������
    ������������ ��� ���������� ASP.NET Core �� ������ 6
*/

// Host.CreateDefaultBuilder()
/*
    �������� ����� ���������� .NET (�� ����������� ���)
    ������� � ����������� IHostBuilder, ������� ����� ������������ ��� ���������� � ���-����������
    ������������� ����������� ������������ � �����������, �� ����� �����������
    �������� ��� ����������, ������� ����� �� �������� ���-������
    ������������� ������������ ��� �������� ������� �����, ���������� � ASP.NET Core ����������, ������� � ������ 3.0
*/

// WebApplication.CreateBuilder()
/*
    ������ � ASP.NET Core 6 ��� ���������� ������ �������� ���-����������
    ������� WebApplicationBuilder, ������������� ����������� Host � WebHost � ����� �������
    ��������� ��������� ������, �������� � middleware
    ������������� �������� ������������ � �����������, ��������� ����� ��������� ���������� ���������� � ����� �����
    ������������� ������������ ��� ����� �������� ASP.NET Core, ��� ��� ���� ������ ����� ����������� � �������� ������� ��������� ����������
    ���������� ����� ��������������� API ��� ��������� ����������, ���������� ���������������� ����� ���������� �������
*/

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
builder.Services.AddFeatureManagement(builder.Configuration.GetSection("FeatureFlags"));

builder.Services.AddQuartzExtension(builder);
builder.Services.AddRedisExtensions(builder);
builder.Services.AddOptionsExtensions(builder);
builder.Services.AddServicesExtensions(builder);
builder.Services.AddRepositoriesExtensions(builder);
builder.Services.AddOpenTelemetryExtension(builder);
IConfiguration configuration = builder.Services.AddConfigurationExtension(builder);


WebApplication app = builder.Build();

builder.Services.AddVaultExtensions(app, builder, configuration);

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
