using Microsoft.Extensions.Options;

namespace OptionConfiguration;

public class ApplicationOptionsSetup : IConfigureOptions<ApplicationOptions>
{
    private readonly IConfiguration _configuration;

    public ApplicationOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(ApplicationOptions options)
    {
        _configuration.GetSection(nameof(ApplicationOptions))
            .Bind(options);
    }
}
