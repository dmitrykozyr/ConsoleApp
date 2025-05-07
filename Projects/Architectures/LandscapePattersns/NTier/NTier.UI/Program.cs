using Microsoft.OpenApi.Models;
using NTier.Logic.Services.Implementation;
using NTier.Logic.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddRazorPages();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gettoe_Elementary", Version = "v1" });
});

builder.Services.AddScoped<IApplicant_Service, Applicant_Service>();
builder.Services.AddScoped<IGrade_Service, Grade_Service>();
builder.Services.AddScoped<IApplication_Service, Application_Service>();
builder.Services.AddScoped<IApplicationStatus_Service, ApplicationStatus_Service>();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gettoe_Elementary v1"));
}

app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
