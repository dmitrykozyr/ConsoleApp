using gRPC.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Используем стандартную HTTPS конфигурацию для gRPC
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<UserServiceImpl>();

Console.WriteLine("gRPC сервер запускается...");
Console.WriteLine("Слушает на: https://localhost:7191 (стандартный HTTPS порт)");

app.Run();
