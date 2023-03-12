using Avonale.Products.Application.Commands;
using Avonale.Products.Application.Queries;
using Avonale.Products.Data;
using Avonale.Products.Data.Repositories;
using Avonale.Products.Domain.Interfaces;
using Core.DomainObjects;
using Core.DomainObjects.Interfaces;
using Core.MediatR;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Avonale.Products.Infra.CrossCutting.Ioc;

public static class DependencyInjectionConfig
{
    public static void ResolveDependencies(this IServiceCollection services)
    {

        services.AddScoped<INotifier, Notifier>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductQueries, ProductQueries>();
        
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IRequestHandler<PurchaseProductCommand, ValidationResult>, PurchaseProductCommandHandler>();
        services.AddScoped<IRequestHandler<RegisterProductCommand, ValidationResult>, RegisterProductCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveProductCommand, ValidationResult>, RemoveProductCommandHandler>();
        services.AddScoped<ProductContext>();
    }
}