using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Presentation.Extensions;

//! Доработать OpenTelemetry и использовать его - выводить телеметрию в Grafana
// сейчас есть только регистрация
public static class OpenTelemetryExtensions
{
    public static void AddOpenTelemetryExtension(this IServiceCollection serviceCollection, WebApplicationBuilder builder)
    {
        builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(nameof(Presentation)))
            .WithMetrics(metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation();

                metrics.AddOtlpExporter();
            })
            .WithTracing(tracing =>
            {
                tracing
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation();

                tracing.AddOtlpExporter();
            });

        builder.Logging.AddOpenTelemetry(logging => logging.AddOtlpExporter());
    }
}
