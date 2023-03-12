using Avonale.Payment.Application.Commands;
using Avonale.Payment.Service.Services;
using Core.MediatR;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Avonale.Payment.Infra.CrossCutting.IoC;

public static class DependencyInjectionConfig
{
    public static void ResolveDependencies(this IServiceCollection services)
    {
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IRequestHandler<PaymentCommand, ValidationResult>, PaymentCommandHandler>();

        services.AddHostedService<PaymentIntegrationHandler>();

    }
}