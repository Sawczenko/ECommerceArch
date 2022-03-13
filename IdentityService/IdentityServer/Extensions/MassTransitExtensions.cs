
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Extensions
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddMassTransitServices(this IServiceCollection services, IConfiguration  configuration)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, config) =>
                {
                    config.Host("amqp://guest:guest@localhost:5672");
                });
            });
            services.AddMassTransitHostedService(true);
            return services;
        }
    }
}