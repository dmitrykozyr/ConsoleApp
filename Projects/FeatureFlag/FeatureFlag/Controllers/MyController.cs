using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;

namespace FeatureFlag.Controllers;

[ApiController]
[Route("[controller]")]
public class MyController : ControllerBase
{
    private readonly IFeatureManager _featureManager;

    public MyController(IFeatureManager featureManager)
    {
        _featureManager = featureManager;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var isFeature1Enabled = await _featureManager.IsEnabledAsync("MyFeature_1");
        var isFeature2Enabled = await _featureManager.IsEnabledAsync("MyFeature_2");

        if (isFeature1Enabled)
        {
            // Do something
        }

        return Ok();
    }
}
