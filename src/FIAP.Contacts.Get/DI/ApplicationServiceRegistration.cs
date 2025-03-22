using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace FIAP.Contacts.Get.DI;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddFunctionService(this IServiceCollection services)
    {

        var serviceName = "fiap-contact-function-get";
        var serviceVersion = "1.0.0";

        // Create a single ActivitySource that can be used throughout the application
        var activitySource = new ActivitySource(serviceName, serviceVersion);
        services.AddSingleton(activitySource);

        var resourceBuilder = ResourceBuilder.CreateDefault()
          .AddService(serviceName: serviceName, serviceVersion: serviceVersion);

        services.AddOpenTelemetry()
               .ConfigureResource(resource => resource.AddService(
                   serviceName: serviceName,
                   serviceVersion: serviceVersion))
               .WithTracing(tracing => tracing
                   .AddSource(serviceName)
                   .AddAspNetCoreInstrumentation()
                   .AddHttpClientInstrumentation()
                   .AddEntityFrameworkCoreInstrumentation(x => x.SetDbStatementForText = true) // Add if using EF Core
                   .AddOtlpExporter(options =>
                   {
                       options.Endpoint = new Uri("http://134.122.121.176:4317");
                       options.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                   }));

        // Configure OpenTelemetry logging
        services.AddLogging(logging =>
        {
            logging.AddOpenTelemetry(options =>
            {
                options.SetResourceBuilder(resourceBuilder);
                options.IncludeFormattedMessage = true;
                options.IncludeScopes = true;

                options.AddOtlpExporter(exporterOptions =>
                {
                    exporterOptions.Endpoint = new Uri("http://134.122.121.176:4317");
                    exporterOptions.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                });
            });
        });

        return services;
    }
}
