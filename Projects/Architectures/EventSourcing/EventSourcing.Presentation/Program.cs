using EventSourcing.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

IServiceCollection serviceCollection = builder.Services;

serviceCollection.AddEndpointsApiExplorer();
serviceCollection.AddSwaggerGen();

serviceCollection.AddServicesExtensions();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
