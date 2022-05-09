using Microsoft.Extensions.Configuration;

namespace NTier.DataAccess.DataContext
{
    public class AppConfiguration
    {
        public AppConfiguration()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            string pathToAppsettings = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(pathToAppsettings, false);
            IConfigurationRoot root = configurationBuilder.Build();
            IConfigurationSection appSettings= root.GetSection("ConnectionStrings:DefaultConnection");
            SqlConnectionString = appSettings.Value;
        }

        public string SqlConnectionString { get; set; }
    }
}
