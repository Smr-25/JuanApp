using JuanApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuanApp.DLL.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
        builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(p => p.ImageUrl).IsRequired();
        builder.Property(p => p.InStock).IsRequired();
        builder.Property(p => p.DiscountPercentage).IsRequired();
        builder.Property(p => p.IsNew).IsRequired();
        builder.Property(p => p.IsMain).IsRequired();
    }
}