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
        builder.Property(s => s.Description).HasMaxLength(500);
        builder.Property(s => s.ButtonText).HasMaxLength(50);
        builder.Property(s => s.ButtonLink).HasMaxLength(200);
        builder.Property(s => s.IsMain).IsRequired();
        builder.HasData(
         new Slider
         {
             Id = 1,
             Title = "Welcome to JuanApp",
             SubTitle = "Quality Software Solutions",
             Description = "Your trusted partner in software solutions.",
             ImageUrl = "slider-1.jpg",
             ButtonText = "Learn More",
             ButtonLink = "/about",
             IsMain = true
         },
         new Slider
         {
             Id = 2,
             Title = "Innovative Solutions",
             SubTitle = "Cutting-Edge Technology",
             Description = "Transforming ideas into reality.",
             ImageUrl = "slider-2.jpg",
             ButtonText = "Our Services",
             ButtonLink = "/services",
             IsMain = true
         },
            new Slider
            {
                Id = 3,
                Title = "Join Our Community",
                SubTitle = "Stay Connected",
                ImageUrl = "banner-3.jpg",
                IsMain = false
            },
            new Slider
            {
                Id = 4,
                Title = "Expert Support",
                SubTitle = "We're Here to Help",
                ImageUrl = "banner-4.jpg",
                IsMain = false
            }
     );
    }
}