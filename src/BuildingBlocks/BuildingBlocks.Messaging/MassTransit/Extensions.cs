using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Messaging.MassTransit;

public static class Extensions
{
    public static IServiceCollection AddMessageBroker(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly? assembly = null
    )
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            if (assembly != null)
            {
                x.AddConsumers(assembly);
            }

            x.UsingRabbitMq(
                (context, cfg) =>
                {
                    cfg.Host(
                        new Uri(configuration["MessageBroker:Host"]!),
                        host =>
                        {
                            host.Username(configuration["MessageBroker:UserName"]!);
                            host.Password(configuration["MessageBroker:Password"]!);
                        }
                    );

                    cfg.ConfigureEndpoints(context);
                }
            );
        });

        return services;
    }
}
