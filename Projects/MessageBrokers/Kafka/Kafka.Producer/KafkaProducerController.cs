using Microsoft.AspNetCore.Mvc;

namespace Kafka.Producer;

[ApiController]
[Route("[controller]")]
public class KafkaProducerController
{
    [HttpGet(Name = "Get")]
    public IResult Get()
    {
        return Results.Ok();
    }
}
