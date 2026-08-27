using CQRS.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentationServices(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
