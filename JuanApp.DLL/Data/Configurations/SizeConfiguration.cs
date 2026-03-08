using JuanApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuanApp.DLL.Data.Configurations;

public class SizeConfiguration : IEntityTypeConfiguration<Size>
{
    public void Configure(EntityTypeBuilder<Size> builder)
    {
        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(50);
        builder.HasMany(s => s.Products)
            .WithMany(p => p.Sizes);
        builder.HasData(
            new Size { Id = 1, Name = "XS" },
            new Size { Id = 2, Name = "S" },
            new Size { Id = 3, Name = "M" },
            new Size { Id = 4, Name = "L" },
            new Size { Id = 5, Name = "XL" },
            new Size { Id = 6, Name = "XXL" }
        );
    }
}