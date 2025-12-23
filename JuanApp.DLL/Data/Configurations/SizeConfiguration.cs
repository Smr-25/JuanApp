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
            new Size { Id = 1, Name = "Small", SizeType = "Clothing" },
            new Size { Id = 2, Name = "Medium", SizeType = "Clothing" },
            new Size { Id = 3, Name = "Large", SizeType = "Clothing" },
            new Size { Id = 4, Name = "X-Large", SizeType = "Clothing" },
            new Size { Id = 5, Name = "6", SizeType = "Shoes" },
            new Size { Id = 6, Name = "7", SizeType = "Shoes" },
            new Size { Id = 7, Name = "8", SizeType = "Shoes" },
            new Size { Id = 8, Name = "9", SizeType = "Shoes" }
        );
    }
}