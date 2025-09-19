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

builder.Services.AddScoped<IApplicantService, ApplicantService>();
builder.Services.AddScoped<IGradeService, GradeService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IApplicationStatusService, ApplicationStatusService>();


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
