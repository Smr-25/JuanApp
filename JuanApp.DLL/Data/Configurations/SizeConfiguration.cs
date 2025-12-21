using JuanApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuanApp.DLL.Data.Configurations;

public class SizeConfiguration : IEntityTypeConfiguration<Size>
{
    public void Configure(EntityTypeBuilder<Size> builder)
    {
        builder.Property(s => s.SizeType)
            .IsRequired()
            .HasMaxLength(50);
        builder.HasMany(s => s.Products)
            .WithMany(p => p.Sizes);
        builder.HasData(
            new Size { Id = 1, SizeType = "XS" },
            new Size { Id = 2, SizeType = "S" },
            new Size { Id = 3, SizeType = "M" },
            new Size { Id = 4, SizeType = "L" },
            new Size { Id = 5, SizeType = "XL" },
            new Size { Id = 6, SizeType = "XXL" }
        );
    }
}