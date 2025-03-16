using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using System.Diagnostics.Metrics;
using System.Diagnostics;

namespace FIAP.Contacts.Get.DI;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddFunctionService(this IServiceCollection services)
    {

        var loggerConfig = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithProperty("ApplicationName", "FIAP.Contacts.Get.Api")
            .WriteTo.Console()
            .CreateLogger();

        services.AddSingleton<ILoggerFactory>(new SerilogLoggerFactory(loggerConfig));
        services.AddLogging();

        return services;
    }
}
