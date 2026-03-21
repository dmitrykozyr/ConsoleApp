using Kafka.Consumer;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<CustomWorker>();

var host = builder.Build();
host.Run();
