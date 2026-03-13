using Microsoft.AspNetCore.Mvc;

namespace Vault_.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultController
{
    private readonly IConfiguration _configuration;

    public VaultController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("GetSecrets")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IResult> AddTimerEntry()
    {
        //!var secret_1 = _configuration.GetSection(nameof(SecretNames.secret_1)).Value;
        //!var secret_2 = _configuration.GetSection(nameof(SecretNames.secret_2)).Value;

        return Results.Ok();
    }
}
