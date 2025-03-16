using FIAP.Contacts.Get.Domain.ContactAggregate;
using FIAP.Contacts.Get.Infra.Context;
using FIAP.Contacts.Get.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Contacts.Get.Infra
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Default")));

            services.AddScoped<IContactRepository, ContactRepository>();

            return services;
        }

        public static IServiceProvider UpdateMigrate(this IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();

            return serviceProvider;
        }
    }
}
