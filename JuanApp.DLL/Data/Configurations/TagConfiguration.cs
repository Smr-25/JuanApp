using JuanApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuanApp.DLL.Data.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.HasMany(t => t.Products)
            .WithMany(t => t.Tags);
        builder.HasData(
            new Tag { Id = 1, Name = "New Arrival" },
            new Tag { Id = 2, Name = "Best Seller" },
            new Tag { Id = 3, Name = "Limited Edition" },
            new Tag { Id = 4, Name = "On Sale" },
            new Tag { Id = 5, Name = "Exclusive" }
        );
    }
}