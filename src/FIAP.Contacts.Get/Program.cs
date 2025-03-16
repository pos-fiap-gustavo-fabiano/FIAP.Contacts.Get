using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FIAP.Contacts.Get.Application.Shared;
using FIAP.Contacts.Get.Infra;
using FIAP.Contacts.Get.DI;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((builder, services) => {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddInfraServices(builder.Configuration);
        services.AddApplicationService();
        services.AddFunctionService();

    })
    .Build();

host.Run();