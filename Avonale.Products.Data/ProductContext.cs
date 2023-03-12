using Avonale.Products.Data.Extensions;
using Avonale.Products.Domain.Entities;
using Core.DomainObjects.Interfaces;
using Core.MediatR;
using Core.Messages;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace Avonale.Products.Data;

public sealed class ProductContext : DbContext, IUnitOfWork
{
    private readonly IMediatorHandler _mediatorHandler;

    public ProductContext(DbContextOptions<ProductContext> options, IMediatorHandler mediatorHandler) : base(options)
    {
        _mediatorHandler = mediatorHandler;
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Event>();
        modelBuilder.Ignore<ValidationResult>();

        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                     e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductContext).Assembly);
    }

    public async Task<bool> Commit(CancellationToken cancellationToken)
    {
        var success = await SaveChangesAsync(cancellationToken) > 0;
        if (success) await _mediatorHandler.PublishEvents(this);
       
        return success;
    }
}