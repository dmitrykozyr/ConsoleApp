using Domain.Interfaces.Db;
using Domain.Models.JsonDeserialize;
using Domain.Models.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Domain.Services;

public class DbConStrService : IDbConStrService
{
    public static DatabaseOptions? DatabaseOptions { get; set; }

    private readonly IConfiguration _configuration;

    private readonly string? DB_PASSWORD;

    public DbConStrService(IConfiguration configuration, IOptions<DatabaseOptions> databaseOptions)
    {
        _configuration = configuration;

        DatabaseOptions = databaseOptions.Value;

        DB_PASSWORD = _configuration.GetSection(nameof(SecretNames.DbPassword)).Value;
    }

    public string GetDbConnectionString()
    {
        string dbConnStr = $"{DatabaseOptions?.DepositaryConnStr};Password={DB_PASSWORD}";

        return dbConnStr;
    }
}
