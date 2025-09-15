using Microsoft.Extensions.Options;

namespace Presentation;

public class ApplicationOptionsSetup<T> : IConfigureOptions<T>
    where T : class
{
    private readonly IConfiguration _configuration;

    public ApplicationOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(T options)
    {
        _configuration.GetSection(typeof(T).Name).Bind(options);
    }
}
