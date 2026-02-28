using Application.Services.Serializer;
using Domain.Interfaces;
using Infrastructure.HttpClient_;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.Cache;
using Infrastructure.Interfaces.Db;
using Infrastructure.Interfaces.Login;
using Infrastructure.LoggingData;
using Infrastructure.Models.Login;
using Infrastructure.Services;
using Infrastructure.Services.API;
using Infrastructure.Services.Cache;
using Infrastructure.Services.Login;

namespace Presentation.Extensions;

public static class ServicesExtensions
{
    public static void AddServicesExtensions(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ILogging, Logging>();
        serviceCollection.AddScoped<IProvider, Provider>();
        serviceCollection.AddScoped<ISqlService, SqlService>();
        serviceCollection.AddScoped<IFilesService, FilesService>();
        serviceCollection.AddScoped<ILoginService, LoginService>();
        serviceCollection.AddScoped<IDbConStrService, DbConStrService>();
        serviceCollection.AddScoped<IMemoryCacheService, MemoryCacheService>(); //! Протестить
        serviceCollection.AddScoped<IRedisService, RedisService>();
        serviceCollection.AddScoped(typeof(IHttpClientData<>), typeof(HttpClientData<>));

        serviceCollection.AddScoped<IXmlMessageSerializer, XmlMessageSerializer>();
    }
}
