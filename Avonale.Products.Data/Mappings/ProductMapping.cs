using Avonale.Products.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Avonale.Products.Data.Mappings;

public class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasColumnType("varchar(200)");
        
        builder.Property(p => p.Description)
            .IsRequired()
            .HasColumnType("varchar(600)");

        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(38,2)");

        builder.Property(p => p.RegisteredAt)
            .IsRequired();
    }
}