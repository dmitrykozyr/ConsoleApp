using Domain.Models.Options;
using Infrastructure.Interfaces.Db;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

public class DbConStrService : IDbConStrService
{
    public static DatabaseOptions? DatabaseOptions { get; set; }

    private readonly IConfiguration _configuration;

    private readonly string? DB_PASSWORD;

    public DbConStrService(IConfiguration configuration, IOptions<DatabaseOptions> databaseOptions)
    {
        _configuration = configuration;

        DatabaseOptions = databaseOptions.Value;

        //!DB_PASSWORD = _configuration.GetSection(nameof(SecretNames.DbPassword)).Value;
        DB_PASSWORD = "123"; // Брать пароль из Vault
    }

    public string GetDbConnectionString()
    {
        string dbConnStr = $"{DatabaseOptions?.ConnectionString};Password={DB_PASSWORD}";

        return dbConnStr;
    }
}
