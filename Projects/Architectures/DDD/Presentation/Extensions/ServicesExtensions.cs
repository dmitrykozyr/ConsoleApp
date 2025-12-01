using Application.Services.Serializer;
using Domain.Interfaces;
using Domain.Interfaces.Cache;
using Domain.Interfaces.Db;
using Domain.Interfaces.Login;
using Domain.Interfaces.Serializer;
using Domain.Interfaces.Services;
using Infrastructure.HttpClient_;
using Infrastructure.LoggingData;
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
