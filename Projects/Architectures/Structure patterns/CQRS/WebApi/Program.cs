using Application;
using Infrastructure;
using Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddInfrastructure()
    .AddPresentation();
//!
// SeriLog
//builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// SeriLog
//app.UseSerilogRequestLogging();

// Minimal API
/*
    app.MapGet("/products", async (ISender sender) =>
    {
        Result<List<ProductResponse>> result = await sender.Send(new GetProductsQuery());
        return Results.Ok(result.Value);
    });

    app.MapPost("/products", async (CreateProductReuquest request, ISender sender) =>
    {
        CreateProductCommand command = request.Adapt<CreateProductCommand>();
        await sender.Send(command);
        return Results.Ok();
    });
*/

app.UseHttpsRedirection();

app.Run();
