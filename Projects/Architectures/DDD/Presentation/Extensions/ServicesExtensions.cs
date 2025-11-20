using Application.Services;
using Application.Services.API;
using Application.Services.Cache;
using Application.Services.Login;
using Domain.Interfaces;
using Domain.Interfaces.Cache;
using Domain.Interfaces.Db;
using Domain.Interfaces.Login;
using Domain.Interfaces.Services;
using Infrastructure.HttpClient_;
using Infrastructure.LoggingData;

namespace Presentation.Extensions;

public static class ServicesExtensions
{
    public static void AddServicesExtensions(this IServiceCollection serviceCollection, WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ILogging,            Logging>();
        builder.Services.AddScoped<IProvider,           Provider>();
        builder.Services.AddScoped<ISqlService,         SqlService>();
        builder.Services.AddScoped<IFilesService,       FilesService>();
        builder.Services.AddScoped<ILoginService,       LoginService>();
        builder.Services.AddScoped<IDbConStrService,    DbConStrService>();
        builder.Services.AddScoped<IMemoryCacheService, MemoryCacheService>(); //! Протестить
        builder.Services.AddScoped<IRedisService,       RedisService>();
        builder.Services.AddScoped(typeof(IHttpClientData<>), typeof(HttpClientData<>));
    }
}
