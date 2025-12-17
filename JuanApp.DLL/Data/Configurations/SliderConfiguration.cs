using JuanApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuanApp.DLL.Data.Configurations;

public class SliderConfiguration : IEntityTypeConfiguration<Slider>
{
    public void Configure(EntityTypeBuilder<Slider> builder)
    {
        builder.Property(s => s.ImageUrl).IsRequired();
        builder.Property(s => s.Title).IsRequired().HasMaxLength(100);
        builder.Property(s => s.SubTitle).IsRequired().HasMaxLength(100);
        builder.Property(s => s.Description).IsRequired().HasMaxLength(500);
        builder.Property(s => s.ButtonText).IsRequired().HasMaxLength(50);
        builder.Property(s => s.ButtonLink).IsRequired().HasMaxLength(200);
    }
}