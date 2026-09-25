using Microsoft.AspNetCore.Mvc;

namespace Kafka.Producer.Controllers;

[ApiController]
[Route("[controller]")]
public class KafkaProducerController
{
    [HttpGet(Name = "Get")]
    public IResult Get() => Results.Ok();
}
