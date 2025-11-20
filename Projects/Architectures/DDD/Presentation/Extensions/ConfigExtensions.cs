namespace Presentation.Extensions;

public static class ConfigExtensions
{
    public static IConfiguration AddConfigurationExtension(this IServiceCollection serviceCollection, WebApplicationBuilder builder)
    {
        string environment = $"appsettings.{builder.Environment.EnvironmentName}.json";

        builder.Configuration.AddJsonFile(environment, optional: false, reloadOnChange: true);
        builder.Configuration.AddEnvironmentVariables(prefix: "VAULT_");

        var cb = new ConfigurationBuilder();
        cb.SetBasePath(Directory.GetCurrentDirectory());

        cb.AddJsonFile(environment,     optional: true, reloadOnChange: true);
        cb.AddJsonFile("secrets.json",  optional: true, reloadOnChange: true);

        IConfiguration configuration = cb.Build();

        return configuration;
    }
}
