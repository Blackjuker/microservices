using System.Reflection;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Play.Common.Settings;

namespace Play.Common.MassTransit
{
    public static class Extensions
    {
        public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services)
        {
            services.AddMassTransit(configure =>
            {
                // automatically register all consumers in the entry assembly
                configure.AddConsumers(Assembly.GetEntryAssembly());


                // configure RabbitMQ 
                configure.UsingRabbitMq((context, cfg) =>
                {
                     var configuration = context.GetService<IConfiguration>();
                    var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                    var rabbitMQSettings = configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
                    cfg.Host(rabbitMQSettings.Host);
                    cfg.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(serviceSettings.ServiceName, false));
                });
            });

            services.AddMassTransitHostedService();

            return services;
        }
    }
}