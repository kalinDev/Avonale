using Avonale.MessageBus;
using Avonale.Payment.Service.Services;
using Core.Util;

namespace Avonale.Payment.API.Extensions.Configurations;

public static class MessageBusConfig
{
    public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
            .AddHostedService<PaymentIntegrationHandler>();
    }
}