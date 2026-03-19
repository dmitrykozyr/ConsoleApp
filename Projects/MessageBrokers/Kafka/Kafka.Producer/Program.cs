using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var producerConfig = new ProducerConfig { BootstrapServers = "localhost:9092" };

var producer = new ProducerBuilder<Null, string>(producerConfig).Build();

builder.Services.AddSingleton(producer);


var app = builder.Build();

app.MapPost("/send", async (string message, [FromServices] IProducer<Null, string> producer) =>
{
    var kafkaMessage = new Message<Null, string> { Value = message };

    var result = await producer.ProduceAsync("test-topic", kafkaMessage);

    return Results.Ok($"Отправлено: {message} в офсет {result.Offset}");
});

app.Run();
